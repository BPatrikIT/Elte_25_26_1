package src.otherpackage;

import static org.junit.jupiter.api.Assertions.*;
import module org.junit.jupiter;
import org.junit.jupiter.api.Test;
import src.math.operation.safe.Increment;

public class IncrementTest {

    @Test
    void testZero() {
        assertEquals(1, Increment.increment(0));
    }

    @Test
    void testMaxValue() {
        assertEquals(Integer.MAX_VALUE, Increment.increment(Integer.MAX_VALUE));
    }
}
