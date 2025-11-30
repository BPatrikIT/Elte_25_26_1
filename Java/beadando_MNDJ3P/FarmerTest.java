import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

class FarmerTest {

    @Test
    void testPlantCropAndWater() {
        Farm farm = new Farm(2, 2);
        Farmer farmer = new Farmer("John", farm);
        Crop corn = new Crop(CropType.CORN);
        
        farmer.plantCrop(0, 0, corn);
        farmer.waterCrop(0, 0);
        
        assertEquals(2, corn.getWaterLevel(), "Crop water level should increase");
    }

    @Test
    void testHarvestRemovesMatureCrop() {
        Farm farm = new Farm(2, 2);
        Farmer farmer = new Farmer("John", farm);
        Crop lettuce = new Crop(CropType.LETTUCE);
        
        farmer.plantCrop(0, 0, lettuce);
        
        // Grow it
        for(int i = 0; i < 10; i++) {
            farmer.waterCrop(0, 0);
            farmer.simulateDay();
        }
        
        farmer.harvestCrop(0, 0);
        
        assertTrue(farm.getPlot(0, 0).isEmpty(), "Plot should be empty after harvest");
        assertEquals(1, farmer.getHarvestedCrops().size(), "Farmer should have collected crop");
    }

    @Test
    void testCleanDeadCrop() {
        Farm farm = new Farm(1, 1);
        Farmer farmer = new Farmer("John", farm);
        Crop strawberry = new Crop(CropType.STRAWBERRY);
        
        farmer.plantCrop(0, 0, strawberry);
        
        // Kill it (limit 1 day)
        farmer.simulateDay(); 
        farmer.simulateDay(); 
        
        assertTrue(farm.getPlot(0, 0).hasDeadCrop());
        
        farmer.cleanPlot(0, 0);
        
        assertTrue(farm.getPlot(0, 0).isEmpty(), "Dead crop should be removed");
    }

    @Test
    void testWaterMostThirstyCrop() {
        Farm farm = new Farm(2, 2);
        Farmer farmer = new Farmer("John", farm);
        Crop c1 = new Crop(CropType.CORN); // -1 (simulated)
        Crop c2 = new Crop(CropType.CORN); // -2 (simulated)
        
        farmer.plantCrop(0, 0, c1);
        farmer.plantCrop(0, 1, c2);
        
        c1.simulateDay(); // water: -1
        c2.simulateDay(); 
        c2.simulateDay(); // water: -2 (lower)
        
        // Initial state check
        assertTrue(c2.getWaterLevel() < c1.getWaterLevel());
        
        farmer.waterMostThirstyCrop();
        
        // c2 was thirsty, should be watered (+2 -> 0)
        assertEquals(0, c2.getWaterLevel()); 
        assertEquals(-1, c1.getWaterLevel());
    }

    static Stream<Arguments> provideSimulationScenarios() {
        return Stream.of(
            // 1. Mindkét növény életben marad, de csak az egyik érett
            // Lettuce (needs 10 days), Corn (needs 15 days). Run 12 days.
            Arguments.of(CropType.LETTUCE, CropType.CORN, 12, true, 1),

            // 2. Mindkét növény életben marad, és mindkettő érett
            // Lettuce (10 days), Strawberry (10 days). Run 11 days.
            Arguments.of(CropType.LETTUCE, CropType.STRAWBERRY, 11, true, 2),

            // 3. Egyik elpusztul, másik él de nem érett
            // Strawberry (dies fast w/o water), Corn (alive, slow). Water Corn only.
            // Logic handled inside test method by specialized watering
            Arguments.of(CropType.STRAWBERRY, CropType.CORN, 5, false, 0),

            // 4. Egyik elpusztul, másik él és érett
            // Strawberry (dies), Lettuce (matures). Water Lettuce only.
            Arguments.of(CropType.STRAWBERRY, CropType.LETTUCE, 12, false, 1),
            
            // 5. Mindkettő elpusztul
            // Don't water either.
            Arguments.of(CropType.STRAWBERRY, CropType.LETTUCE, 10, false, 0) // No water logic
        );
    }

    /**
     * Paraméterezett teszt a forgatókönyvekhez.
     * @param type1 Az első növény típusa
     * @param type2 A második növény típusa
     * @param days Napok száma
     * @param waterBoth Ha igaz, mindkettőt locsoljuk, ha hamis, csak a második típust (így az első elpusztulhat)
     *                 vagy egyiket sem (ha 'days' elég nagy a halálhoz de waterBoth false).
     *                 Specifikus logika: Ha waterBoth false, feltételezzük a teszt logikájában, 
     *                 hogy szándékosan hagyjuk meghalni az elsőt (kivéve 5. eset).
     * @param expectedHarvest Várt betakarítási mennyiség
     */
    @ParameterizedTest
    @MethodSource("provideSimulationScenarios")
    void testFarmerSimulationWithHarvest(CropType type1, CropType type2, int days, boolean waterBoth, int expectedHarvest) {
        Farm farm = new Farm(1, 2);
        Farmer farmer = new Farmer("Tester", farm);
        
        farmer.plantCrop(0, 0, new Crop(type1));
        farmer.plantCrop(0, 1, new Crop(type2));

        boolean killBoth = !waterBoth && expectedHarvest == 0;

        for (int i = 0; i < days; i++) {
            if (waterBoth) {
                farmer.waterCrop(0, 0);
                farmer.waterCrop(0, 1);
            } else if (!killBoth) {
                // Csak a másodikat locsoljuk, hogy az első meghaljon (scenario 3, 4)
                farmer.waterCrop(0, 1);
            }
            // Ha killBoth, senkit nem locsolunk
            
            farmer.simulateDay();
        }

        farmer.harvestCrops(type1);
        farmer.harvestCrops(type2);

        assertEquals(expectedHarvest, farmer.getHarvestedCrops().size());
    }
}