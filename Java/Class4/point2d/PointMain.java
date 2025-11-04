package point2d;

public class PointMain {
    public static void main(String[] args) {
        Point point = new Point(Double.parseDouble(args[0]), Double.parseDouble(args[1]));
        IO.println(point.print("The point is at: ", point));

        int len = (args.length / 2) - 1;
        /*
        for (int i = 0; i < len; ++i){
            Point shiftPoint = new Point(Double.parseDouble(args[2*i + 2]), Double.parseDouble(args[2*i + 3]));
            point.shift(shiftPoint.x, shiftPoint.y);
            IO.println(point.print("At step #%d after being shifted by".formatted(i+1), shiftPoint));
        }
        */
        Point shiftPoint = new Point(Double.parseDouble(args[2]), Double.parseDouble(args[3]));
        Point mirrorPoint = new Point(Double.parseDouble(args[4]), Double.parseDouble(args[5]));

        point.shift(shiftPoint.x, shiftPoint.y);
        point.mirror(mirrorPoint.x, mirrorPoint.y);
        IO.println(point.print("After being shifted by", shiftPoint));
        IO.println(point.print("and then mirrored by", mirrorPoint));
    }
}
