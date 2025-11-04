void main(String... args) {
    int len = (args.length / 2);

    double sumDistance = 0.0;
    for (int i = 0; i < len; ++i){
        Point point1 = new Point(Double.parseDouble(args[2*i]), Double.parseDouble(args[2*i + 1]));
        Point point2 = new Point(Double.parseDouble(args[2*i + 2]), Double.parseDouble(args[2*i + 3]));
        double distance = point1.distance(point2);
        sumDistance += distance;
        IO.println("With a total distance of %2.1f and a distance of %2.2f from".formatted(sumDistance, distance, point1.print(), point2.print()));
    }
}