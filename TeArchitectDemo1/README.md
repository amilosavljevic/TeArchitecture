# Demo 1 - Event Bus

Idea is to have one action per domain event.

## Questions
- Should one action/message be handled by multiple handlers?
- If so, should we be able to define handler priority?
- Should ITask inherit from IAsyncTask(or whatever) so we can use it with await.
- Should SellPlayerAction carry information what squad to use or we will let handler figure it out?
- Action result would benefit from Type Union (Tagged union/sum types) whateva they are called. That would be useful for error handling.
- Should we change model before we get response from server and do roll-back if transaction fail or should we just do entire transaction when it is approved?
- Show should we call simple message structs. I called it SellPlayerNowAction, but maybe theres better name.
- Currently if user does not mark task as finished, it will never return.
- I would be really nice if we could add custom interface to some proto classes.(We could, just need to implement it). It would be nice to group all responses that have IsSuccess under one interface.
- Maybe remove ITask.Fail(string errorMessage) // only left IError override.

## TODOs
- Check how can we backport C# 8.0 features to .netStandar2.0
	- https://stu.dev/csharp8-doing-unsupported-things/
- Do Unit tests
- Implement simple Bus in order for tests to work
- Implement simple model (only keep data and register handlers)
- Subscribe handlers as model parts on model creation

## Answered questions
