package pointless;
//import point2d.Point;

public class AnotherMain {
    public static void main(String... args) {
        //Point point = new Point(Double.parseDouble(args[0]), Double.parseDouble(args[1]));
        //IO.println(point.print("The point is at: ", point));

        point2d.Point shiftPoint = new point2d.Point(Double.parseDouble(args[0]), Double.parseDouble(args[1]));
        IO.println(shiftPoint.print("After being shifted by", shiftPoint));
    }
}