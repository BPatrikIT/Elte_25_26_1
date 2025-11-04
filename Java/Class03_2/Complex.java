public class Complex {
    double re;
    double im;

    public Complex(double re, double im) {
        this.re = re;
        this.im = im;
    }

    public Complex abs() {
        return new Complex(Math.pow(re, 2), Math.pow(im, 2));
    }

    public Complex add(Complex c) {
        return new Complex(this.re + c.re, this.im + c.im);
    }

    public Complex sub(Complex c) {
        return new Complex(this.re - c.re, this.im - c.im);
    }

    public Complex mul(Complex c) {
        return new Complex(this.re * c.re - this.im * c.im, this.re * c.im + this.im * c.re);
    }

    public String getText() {
        return "%2.1f + %2.1fi".formatted(re, im);
    }
}