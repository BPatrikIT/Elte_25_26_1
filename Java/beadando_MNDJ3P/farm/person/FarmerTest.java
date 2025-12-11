package farm.person;

import farm.crop.Crop;
import farm.crop.CropType;
import farm.field.Farm;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.CsvSource;

import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

public class FarmerTest {

    @Test
    void testPlantCropAndWater() {
        Farm farm = new Farm(2, 2);
        Farmer farmer = new Farmer("John", farm);
        
        farmer.plantCrop(0, 0, CropType.CORN);
        farmer.waterCrop(0, 0);

        Crop planted = farm.getPlot(0, 0).getCrop();

        assertEquals(2, planted.getWaterLevel(), "Crop water level should increase");
    }

    @Test
    void testHarvestRemovesMatureCrop() {
        Farm farm = new Farm(2, 2);
        Farmer farmer = new Farmer("John", farm);

        farmer.plantCrop(0, 0, CropType.LETTUCE);
        Crop crop = farm.getPlot(0, 0).getCrop();

        for (int i = 0; i < 10; i++) {
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

        farmer.plantCrop(0, 0, CropType.STRAWBERRY);

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

        farmer.plantCrop(0, 0, CropType.CORN);
        farmer.plantCrop(0, 1, CropType.CORN);

        Crop c1 = farm.getPlot(0, 0).getCrop();
        Crop c2 = farm.getPlot(0, 1).getCrop();

        c1.simulateDay(); 
        c2.simulateDay();
        c2.simulateDay();

        assertTrue(c2.getWaterLevel() < c1.getWaterLevel());

        farmer.waterMostThirstyCrop();

        assertEquals(0, c2.getWaterLevel());
        assertEquals(-1, c1.getWaterLevel());
    }

    @ParameterizedTest
    @CsvSource({
        "LETTUCE,CORN,12,true,1",
        "LETTUCE,STRAWBERRY,11,true,2",
        "STRAWBERRY,CORN,5,false,0",
        "STRAWBERRY,LETTUCE,12,false,1",
        "STRAWBERRY,LETTUCE,10,false,0"
    })
    void testStory(CropType type1, CropType type2, int days, boolean waterBoth, int expectedHarvest) {
        Farm farm = new Farm(1, 2);
        Farmer farmer = new Farmer("Tester", farm);

        farmer.plantCrop(0, 0, CropType.valueOf(type1.name()));
        farmer.plantCrop(0, 1, CropType.valueOf(type2.name()));

        Crop crop1 = farm.getPlot(0, 0).getCrop();
        Crop crop2 = farm.getPlot(0, 1).getCrop();

        boolean killBoth = !waterBoth && expectedHarvest == 0;

        for (int i = 0; i < days; i++) {
            if (waterBoth) {
                crop1.water();
                crop2.water();
            } else if (!killBoth) {
                crop2.water();
            }
            farmer.simulateDay();
        }

        farmer.harvestCrops(type1);
        farmer.harvestCrops(type2);

        assertEquals(expectedHarvest, farmer.getHarvestedCrops().size());
    }
}
