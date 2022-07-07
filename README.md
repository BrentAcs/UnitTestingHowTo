## Unit Testing How To

### Why unit tests are important.
- Improved code quality
- Saves developer time / company money
- Provides supporting documentation
- Promotes re-usability and reliability
- Reduces code complexity

### Why do we write unit tests?
- Test code in isolation
  - testing code in concert is an integration test, not a unit test
- Pushes us to break code into smaller/testable pieces
- Verify code preforms correctly as early in the development cycle as possible
- When used properly, Unit tests become two things:
    - A sort of ever running acceptance criteria.
    - a sort of developers documentation
- it's a super-fun challenge that rewards us in knowing our code is a SOLID as possible before we hit 'Create' on the PR.

### What don't we test?
- Only methods with return values
- One happy path such that the PR passes a PR gate

### What do we test?
- All the things
- All public class methods
  - If properly covered, private/protected methods *should* be covered by default
- All conceivable cases:
    - Valid situations (happy paths), examples:
        - Was the return value correct
        - Where any out/ref parameters success
        - Were external services called with expected values
    - Invalid situations (unhappy paths)
        - Bad data (null, empty, incorrect format, etc..)
        - Unexpected results from calls
        - Exceptions from calls
    - Exceptional cases
        - Exceptions thrown are of known types
        - Handles handle-able exceptions 

### How to identify what to test?
- Ask ourselves three questions:
  - What are the happy paths?
    - What is the complete set of inputs that it expects
      - What data will exercise every path thru a call?
    - What are the expected results for external calls
  - What are the unhappy paths?
    - What values supplied as args could be
      - null?
      - Out of range?
      - Improperly formatted?
      - etc...
    - For external calls:
      - What could go wrong?         
  - What exceptions are expected?
    - For bad data?
    - Made by external calls

### So. how *do* write unit tests?
- Triple A
  - Arrange
  - Act
  - Assert
- The holy grail: Red, Green, Refactor
- Types of tests (using XUnit terminology)
  - Fact
    - Test a single condition
  - Theory
    - Test a set of related conditions (cases)

### Demonstration Topics
- FluentAssertions
- Mocking (Moq)
- Testing assertions

### Refs:
unit test naming:  https://dzone.com/articles/7-popular-unit-test-naming