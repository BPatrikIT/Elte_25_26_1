public class Farm {
    private final Plot[][] plots;
    private final int rows;
    private final int cols;

    public Farm(int rows, int cols) {
        if (rows < 1 || cols < 1) {
            throw new IllegalArgumentException("Rows and columns must be at least 1");
        }
        this.rows = rows;
        this.cols = cols;
        this.plots = new Plot[rows][cols];
        
        // Parcellák inicializálása
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                plots[i][j] = new Plot();
            }
        }
    }

    public void simulateDay() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                Crop crop = plots[i][j].getCrop();
                if (crop != null) {
                    crop.simulateDay();
                }
            }
        }
    }

    public Crop findMostThirstyCrop() {
        Crop mostThirsty = null;
        int minWater = Integer.MAX_VALUE;

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                Crop crop = plots[i][j].getCrop();
                if (crop != null && crop.isAlive()) {
                    if (crop.getWaterLevel() < minWater) {
                        minWater = crop.getWaterLevel();
                        mostThirsty = crop;
                    }
                }
            }
        }
        return mostThirsty;
    }

    public Plot getPlot(int row, int col) {
        if (row < 0 || row >= rows || col < 0 || col >= cols) {
            throw new IllegalArgumentException("Invalid plot coordinates");
        }
        return plots[row][col];
    }

    public int getRows() {
        return rows;
    }

    public int getCols() {
        return cols;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                sb.append(plots[i][j].toString());
                if (j < cols - 1) {
                    sb.append(" ");
                }
            }
            if (i < rows - 1) {
                sb.append("\n");
            }
        }
        return sb.toString();
    }
}