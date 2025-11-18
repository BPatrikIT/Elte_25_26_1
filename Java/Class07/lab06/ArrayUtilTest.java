import static org.junit.jupiter.api.Assertions.*;
import module org.junit.jupiter;

public class ArrayUtilTest {

    @Test
    public void maxLength0() {
        int[] array = {};
        assertEquals(0, ArrayUtil.max(array));
        assertEquals(0, ArrayUtil.max2(array));
        assertEquals(0, ArrayUtil.max3(array));
        assertEquals(0, ArrayUtil.max4(array));
    }

    @ParameterizedTest
    @CsvSource({
        "5",
        "-3",
        "1000000",
        "-1000000"
    })
    public void maxLength1(int value) {
        int[] array = {value};
        assertEquals(array[0], ArrayUtil.max(array));
        assertEquals(array[0], ArrayUtil.max2(array));
        assertEquals(array[0], ArrayUtil.max3(array));
        assertEquals(array[0], ArrayUtil.max4(array));
        
    }

    @ParameterizedTest
    @CsvSource({
        "1, 5",
        "5, 1"
    })
    public void maxLength2(int value1, int value2) {
        int [] array = {value1, value2};
        int expectedMax = Math.max(value1, value2);
        assertEquals(expectedMax, ArrayUtil.max(array));
        assertEquals(expectedMax, ArrayUtil.max2(array));
        assertEquals(expectedMax, ArrayUtil.max3(array));
        assertEquals(expectedMax, ArrayUtil.max4(array));
    }

    @Test
    public void minMaxLength0() {
        int[] array = {};
        int[] expected = {0, 0};
        assertArrayEquals(expected, ArrayUtil.minMax(array));
    }

    @ParameterizedTest
    @CsvSource({
        "5",
        "-3",
        "1000000",
        "-1000000"
    })
    public void minMaxLength1(int value) {
        int[] array = {value};
        int[] expected = {value, value};
        assertArrayEquals(expected, ArrayUtil.minMax(array));
    }

    @ParameterizedTest
    @CsvSource({
        "1, 5",
        "5, 1",
        "-10, 10",
        "0, 0"
    })
    public void minMaxLength2(int value1, int value2) {
        int [] array = {value1, value2};
        int expectedMin = Math.min(value1, value2);
        int expectedMax = Math.max(value1, value2);
        int[] expected = {expectedMin, expectedMax};
        assertArrayEquals(expected, ArrayUtil.minMax(array));
    }

}