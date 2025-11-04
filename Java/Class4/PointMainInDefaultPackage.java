import point2d.Point;

public class PointMainInDefaultPackage {
    public static void main(String[] args) {
        Point point = new Point(1.0, 2.0);
        IO.println(point.print());
    }
}