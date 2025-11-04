package plane;

public class PublicCircle {
    public double x;
    public double y;
    public double r;

    public PublicCircle() {
        this.x = 0;
        this.y = 0;
        this.r = 1;
    }

    public double getArea() {
        return Math.PI * r * r;
    }

}