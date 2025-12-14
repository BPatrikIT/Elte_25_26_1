package farm.person;

import farm.crop.Crop;
import farm.field.Plot;
import farm.field.Farm;
import farm.crop.CropType;

import java.util.ArrayList;
import java.util.List;

public class Farmer {
    private final String name;
    private final Farm farm;
    private final ArrayList<Crop> harvestedCrops;

    public String getName() {
        return name;
    }

    public Farmer(String name, Farm farm) {
        if (name == null || farm == null) {
            throw new IllegalArgumentException("Name and Farm cannot be null");
        }
        this.name = name;
        this.farm = farm;
        this.harvestedCrops = new ArrayList<>();
    }

    public void plantCrop(int row, int col, CropType type) {
        farm.getPlot(row, col).plant(new Crop(type));
    }

    public void waterCrop(int row, int col) {
        Crop crop = farm.getPlot(row, col).getCrop();
        if (crop != null && !crop.getIsCropDead()) {
            crop.water();
        }
    }

    public void waterCrops(CropType type) {
        for (int i = 0; i < farm.getRows(); i++) {
            for (int j = 0; j < farm.getCols(); j++) {
                Crop crop = farm.getPlot(i, j).getCrop();
                if (crop != null && !crop.getIsCropDead() && crop.getType() == type) {
                    crop.water();
                }
            }
        }
    }

    public void harvestCrop(int row, int col) {
        Plot plot = farm.getPlot(row, col);
        Crop crop = plot.getCrop();
        
        if (crop != null && crop.harvest()) {
            harvestedCrops.add(crop);
            plot.removeCrop();
        }
    }

    public void harvestCrops(CropType type) {
        for (int i = 0; i < farm.getRows(); i++) {
            for (int j = 0; j < farm.getCols(); j++) {
                Plot plot = farm.getPlot(i, j);
                Crop crop = plot.getCrop();
                
                if (crop != null && crop.getType() == type) {
                    if (crop.harvest()) {
                        harvestedCrops.add(crop);
                        plot.removeCrop();
                    }
                }
            }
        }
    }

    public void cleanPlot(int row, int col) {
        Plot plot = farm.getPlot(row, col);
        if (plot.hasDeadCrop()) {
            plot.removeCrop();
        }
    }

    public void simulateDay() {
        farm.simulateDay();
    }

    public void waterMostThirstyCrop() {
        Crop thirsty = farm.findMostThirstyCrop();
        if (thirsty != null) {
            thirsty.water();
        }
    }

    public ArrayList<Crop> getHarvestedCrops() {
        return new ArrayList<>(harvestedCrops);
    }
}