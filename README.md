# TeArchitecture

Playing with new ideas for TE architecture.

Doc: https://docs.google.com/document/d/1DmfnPwsOaGEyJ6JXelsjNWU8jjWk_K19VtPLea76shs/edit

## Project Content

### TeArchitecture.Shared

Most important project. Holds ideas that I want to test + some mock or simple implementation.

Folders:
- "Concepts": Interface definition for stuff that we want to test.
- "Mock": Couple of Mock implementations for some of the concepts. Did it in order to actually run unit tests.
- "SimpleImplementations": Self-explanatory


### TeArchitect.Demo1

Project that have couple of classes that looks like real use cases from TE. Soluction are re-implemented in a new fashion, in order to test design ideas.
"Demo1" have a new MD file that is used to keep track of what I wanted to try.

### TeArchitect.Demo1.Test

Unit test for "TeArchitect.Demo1" project.

### TeArchitecture.Domain

Some domain specific entities and value-classes specific to TE (like Player, PlayerId, Morale, Condition...)