void main(String... args) {
    Complex c1 = new Complex(4.0, 2.0);
    IO.println(c1.abs().getText());
    c1 = c1.add(new Complex(1.0, -13.0));
    IO.println("The number is " + c1.getText() + " after adding " + new Complex(1.0, -13.0).getText());
    c1 = c1.sub(new Complex(11.1, 2.4));
    IO.println("The number is " + c1.getText() + " after subtracting " + new Complex(11.1, 2.4).getText());
    c1 = c1.mul(new Complex(0.0, 1.0));
    IO.println("The number is " + c1.getText() + " after multiplying with " + new Complex(0.0, 1.0).getText());
}