public class Point {
    public double x;
    public double y;

    public Point(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public String print() {
        return "At (%2.1f, %2.1f)".formatted(x, y);
    }

    public void shift(double dx, double dy) {
        x += dx;
        y += dy;
    }

    public String print(String text, Point otherPoint) {
        return "%s %s (%2.1f, %2.1f)".formatted(print(), text, otherPoint.x, otherPoint.y);
    }

    public void mirror(double cx, double cy) {
        x = 2 * cx - x;
        y = 2 * cy - y;
    }

    public double distance(Point p) {
        return Math.sqrt(Math.pow(x - p.x, 2) + Math.pow(y - p.y, 2));
    }
}