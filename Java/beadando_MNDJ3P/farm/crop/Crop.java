package farm.crop;

public class Crop {
    private final CropType type;
    private int growthLevel;
    private int waterLevel;
    private int dryDays;
    private boolean isCropDead;

    public Crop(CropType type) {
        if (type == null) {
            throw new IllegalArgumentException("CropType cannot be null");
        }
        this.type = type;
        this.growthLevel = 0;
        this.waterLevel = 0;
        this.dryDays = 0;
        this.isCropDead = false;
    }

    public void water() {
        if (isCropDead) return;
        this.waterLevel = Math.min(10, this.waterLevel + 2);
        this.dryDays = 0;
    }

    public void simulateDay() {
        if (isCropDead) return;

        if (waterLevel > 0) {
            growthLevel += type.getGrowthRate();
        } else {
            dryDays++;
        }
        
        waterLevel--;

        checkSurvival();
    }

    private void checkSurvival() {
        int limit = switch (type) {
            case STRAWBERRY -> 1;
            case LETTUCE -> 2;
            case CORN -> 3;
        };

        if (dryDays > limit) {
            isCropDead = true;
        }
    }

    public boolean isMature() {
        return !isCropDead && growthLevel >= type.getPossibleMaturity();
    }

    public boolean harvest() {
        if (isMature()) {
            // Visszaállítjuk az alapértékeket
            this.waterLevel = 0;
            this.growthLevel = 0;
            this.dryDays = 0;
            return true;
        }
        return false;
    }

    public boolean getIsCropDead() {
        return isCropDead;
    }


    public CropType getType() {
        return type;
    }

    public int getWaterLevel() {
        return waterLevel;
    }

    public int getGrowthLevel() {
        return growthLevel;
    }

    public int getDryDays() {
        return dryDays;
    }

    @Override
    public String toString() {
        if (isCropDead) {
            return "D";
        }
        return switch (type) {
            case LETTUCE -> "L";
            case CORN -> "C";
            case STRAWBERRY -> "S";
        };
    }
}