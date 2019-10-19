# Demo 1 - Event Bus

Idea is to have one action per domain event.

## Questions
- Should one action/message be handled by multiple handlers?
- If so, should we be able to define handler priority?
- Should IResult inherit from ITask so we can use it with await.
- Should SellPlayerAction carry information what squad to use or we will let handler figure it out?
- Action result would benefit from Type Union (Tagged union/sum types) whateva they are called. That would be useful for error handling.
- Should we change model before we get response from server and do roll-back if transaction fail or should we just do entire transaction when it is approved?

## TODOs
- Kill IResultBuilder and provide better design for it. It's too complex that way.
- Find better way to make dev declare what is result of his handler.
- Find better way to explain what went wrong during action.
- Check how can we backport C# 8.0 features to .netStandar2.0
	- https://stu.dev/csharp8-doing-unsupported-things/
-


## Answered questions
