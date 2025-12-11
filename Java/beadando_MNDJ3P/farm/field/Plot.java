package farm.field;

import farm.crop.Crop;
import farm.crop.CropType;

public class Plot {
    private Crop crop;

    public void plant(Crop crop) {
        if (crop == null) {
            throw new IllegalArgumentException("Cannot plant null crop");
        }
        if (this.crop != null) {
            throw new IllegalStateException("Plot is already occupied");
        }
        this.crop = crop;
    }

    public void removeCrop() {
        this.crop = null;
    }

    public boolean hasDeadCrop() {
        return crop != null && crop.getIsCropDead();
    }

    public boolean isEmpty() {
        return crop == null;
    }

    public Crop getCrop() {
        return crop;
    }

    @Override
    public String toString() {
        if (isEmpty()) {
            return "E";
        }
        return crop.toString();
    }
}