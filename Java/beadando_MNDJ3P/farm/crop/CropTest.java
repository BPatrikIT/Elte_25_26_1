package farm.crop;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class CropTest {

    @Test
    void testWaterDecayAfterSimulateDay() {
        Crop crop = new Crop(CropType.CORN);
        crop.water(); // Level 2
        int initialWater = crop.getWaterLevel();
        
        crop.simulateDay();
        
        assertEquals(initialWater - 1, crop.getWaterLevel(), "Water level should decrease by 1");
    }

    @Test
    void testGrowthOccursWhenWatered() {
        Crop crop = new Crop(CropType.LETTUCE); // Growth rate 3
        crop.water(); // Water 2
        
        crop.simulateDay();
        
        assertEquals(3, crop.getGrowthLevel(), "Growth level should increase by growthRate when watered");
    }

    @Test
    void testDifferentDeathThresholds() {
        // STRAWBERRY: Limit 1 day
        Crop strawberry = new Crop(CropType.STRAWBERRY);
        strawberry.simulateDay(); // Dry 1 -> OK
        assertFalse(strawberry.getIsCropDead());
        strawberry.simulateDay(); // Dry 2 -> Dead (2 > 1)
        assertTrue(strawberry.getIsCropDead());

        // LETTUCE: Limit 2 days
        Crop lettuce = new Crop(CropType.LETTUCE);
        lettuce.simulateDay(); // Dry 1
        lettuce.simulateDay(); // Dry 2 -> OK
        assertFalse(lettuce.getIsCropDead());
        lettuce.simulateDay(); // Dry 3 -> Dead (3 > 2)
        assertTrue(lettuce.getIsCropDead());
    }

    @Test
    void testIsMature() {
        Crop lettuce = new Crop(CropType.LETTUCE); // Maturity at 30, Rate 3
        
        // 10 days of perfect growth
        for(int i = 0; i < 10; i++) {
            lettuce.water();
            lettuce.simulateDay();
        }
        
        assertTrue(lettuce.isMature(), "Lettuce should be mature at level 30");
    }

    @Test
    void testHarvestFailsWhenNotMatureOrDead() {
        Crop crop = new Crop(CropType.CORN);
        assertFalse(crop.harvest(), "Cannot harvest immature crop");
        
        // Kill crop
        for(int i=0; i<10; i++) crop.simulateDay();
        
        assertFalse(!crop.getIsCropDead());
        assertFalse(crop.harvest(), "Cannot harvest dead crop");
    }

    @Test
    void testHarvestSuccess() {
        Crop crop = new Crop(CropType.LETTUCE); // Rate 3, Target 30
        for(int i = 0; i < 10; i++) {
            crop.water();
            crop.simulateDay();
        }
        
        assertTrue(crop.harvest(), "Should return true on successful harvest");
        // Check reset
        assertEquals(0, crop.getWaterLevel());
        assertEquals(0, crop.getGrowthLevel());
        assertEquals(0, crop.getDryDays());
    }
}