package plane.but.not.flying;

import plane.PublicCircle;

public class CircleMain {
    public static void main(String[] args) {
        PublicCircle circle = new PublicCircle();
        IO.println("Area: %2.1f".formatted(circle.getArea()));
    }
}