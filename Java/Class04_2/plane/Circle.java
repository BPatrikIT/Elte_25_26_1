package plane;

public class Circle {
    private double x;
    private double y;
    private double r;

    public Circle() {
        this.x = 0;
        this.y = 0;
        this.r = 1;
    }

    public double getArea() {
        return Math.PI * r * r;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public double getR() {
        return r;
    }

    public void setX(double x) {
        this.x = x;
    }

    public void setY(double y) {
        this.y = y;
    }

    public void setR(double r) {
        if (r <= 0.0) {
            throw new IllegalArgumentException("Radius cannot be null or negative");
        }
        this.r = r;
    }

}