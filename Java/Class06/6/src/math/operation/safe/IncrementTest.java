package src.math.operation.safe;

import static org.junit.jupiter.api.Assertions.*;
import module org.junit.jupiter;
import org.junit.jupiter.api.Test;

public class IncrementTest {

    @Test
    void testZero() {
        assertEquals(1, Increment.increment(0), "0 → 1");
    }

    @Test
    void testMinValue() {
        assertEquals(Integer.MIN_VALUE + 1, Increment.increment(Integer.MIN_VALUE),
                "MIN_VALUE → MIN_VALUE + 1");
    }

    @Test
    void testMaxValue() {
        assertEquals(Integer.MAX_VALUE, Increment.increment(Integer.MAX_VALUE),
                "MAX_VALUE → MAX_VALUE (no overflow)");
    }

    @Test
    void testLargePositive() {
        assertEquals(1000001, Increment.increment(1000000),
                "1 000 000 → 1 000 001");
    }

    @Test
    void testLargeNegative() {
        assertEquals(-999999, Increment.increment(-1000000),
                "-1 000 000 → -999 999");
    }

    @Test
    void testMinusOne() {
        assertEquals(0, Increment.increment(-1), "-1 → 0");
    }
}