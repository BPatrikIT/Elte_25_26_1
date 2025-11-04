package src.math.operation.safe;

public class Increment {

    public static int increment(int x) {
        if (x == Integer.MAX_VALUE) {
            return Integer.MAX_VALUE;
        }
        return x + 1;
    }
}