public class ArrayUtil {
    public static int max(int[] array) {
        if (array.length == 0) {
            return 0;
        }

        int max = Integer.MIN_VALUE;
        for (int i = 0; i < array.length; i++) {
            if (array[i] > max) {
                max = array[i];
            }
        }
        return max;
    }

    public static int max2(int[] array) {
        if (array.length == 0) {
            return 0;
        }

        int max = Integer.MIN_VALUE;
        for (int i = 0; i < array.length; i++) {
            max = (array[i] > max) ? array[i] : max;
        }
        return max;
    }

    public static int max3(int[] array) {
        if (array.length == 0) {
            return 0;
        }

        int max = Integer.MIN_VALUE;
        for (int i = 0; i < array.length; i++) {
            max = Math.max(array[i], max);
        }
        return max;
    }

    public static int max4(int[] array) {
        if (array.length == 0) {
            return 0;
        }
        int max = Integer.MIN_VALUE;

        for (int value : array) {
            max = Math.max(value, max);
        }
        return max;
    }

    public static int[] minMax(int[] array) {
        if (array.length == 0) {
            return new int[]{0, 0};
        }
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;

        for (int value : array) {
            min = Math.min(value, min);
            max = Math.max(value, max);
        }
        return new int[]{min, max};
    }
}