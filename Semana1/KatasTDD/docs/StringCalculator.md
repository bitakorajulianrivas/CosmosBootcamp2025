# String Calculator

SOURCE: https://www.codurance.com/katas/string-calculator

## Instructions
### Step 1: Simple Calculator
Create a simple String calculator with a single method:

    class StringCalculator {
        int Add(string numbers);
    }

The method can take 1 or 2 comma-separated numbers, and will return their sum.

The method returns 0 when passed the empty string.

Example:

    Add("") // 0
    Add("4") // 4
    Add("1,2") // 3

Start with the simplest test case of an empty string and move to 1 and two numbers.