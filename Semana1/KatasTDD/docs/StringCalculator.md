# String Calculator
Let’s write a calculator that takes a string and evaluates it as if it were a calculation. The idea is to implement it a bit at a time with tests, and see where the design ends up. The kata description contains quite a detailed list of requirements and examples, written in the order you should implement them in.

Don’t skip ahead or try to design more code than you need for the current tests. Your goal is to learn about incremental design and TDD from this exercise, not actually write a complete solution.
The examples given are supposed to help you think about what’s needed at each step of the solution. You can add more similar examples if you think the ones given aren’t enough.

Note: if your language has a built-in ‘eval’ or another kind of runtime evaluation of string expressions - you’re not allowed to use it in your solution to this exercise!

SOURCE: https://www.codurance.com/katas/string-calculator

## Step 0 - Integers
Let’s start with the basics! If you give your String Calculator a single integer, just return that integer.

- “1” -> 1
- “456” -> 456
- “-2” -> -2