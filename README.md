## Unit Testing How To

### Why unit tests are important.
- Why we leverage them for improved code quality
- How we leverage them for improved code quality

### Why do we write unit tests?
- improve quality
- test code in isolation
  - testing code in concert is an integration test, not a unit test
- pushes us to break code into smaller/testable pieces
- verify code preforms correctly in:
- When used properly, Unit tests become two things:
    - A sort of ever running acceptance criteria.
    - a sort of developers documentation
- it's a super-fun challenge that rewards us in knowing our code is a SOLID as possible before we hit 'Create' on the PR.

### What don't we test?
- only methods with return values
- one happy path so that it 'works'

### What do we test?
- all the things
- all public class methods
  - If properly covered, private methods *should* be covered by default
- all conceivable cases:
    - valid situations (happy paths), examples:
        - Was the return value correct
        - where any out/ref parameters susccess
        - Were external services called with expected values
    - invalid situations (unhappy paths)
        - bad data (null, empty, incorrect format, etc..)
        - unexpected results from calls
        - exceptions from calls
    - exceptional cases
        - exceptions thrown are of known types
        - handles

### How to identify what to test?
- Ask ourselves three questions:
  - what are the happy paths?
    - what is the complete set of inputs that it expects
      - what data will exercise every path thru a call?
    - what are the expected results for external calls
  - what are the unhappy paths?
    - what values supplied as args could be
      - null?
      - out of range?
      - improperly formatted?
    - for external calls:
      - what could go wrong?         
  - what exceptions are expected?
    - for bad data?
    - made by external calls

### So. how *do* write unit tests?
- Triple A
  - Arrange
  - Act
  - Assert
- The holy grail: Red, Green, Refactor
- Types of tests (using XUnit terminology)
  - Fact
    - test a single condition
  - Theory
    - test a set of related conditions (cases)

### Demonstration Topics
- FluentAssertions
- Mocking (Moq)
- Testing assertions

### Refs:
unit test naming:  https://dzone.com/articles/7-popular-unit-test-naming