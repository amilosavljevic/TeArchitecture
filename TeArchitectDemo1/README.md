# Demo 1 - Event Bus

Idea is to have one action per domain event.

## Questions
- Should ITask inherit from Task(or whatever) so we can use it with await.
- Should we change model before we get response from server and do roll-back if transaction fail or should we just do entire transaction when it is approved?
- Show should we call simple message structures? I called it SellPlayerNowAction, but maybe theres a better name.
- Currently if user does not mark task as finished, it will never return.
- I would be really nice if we could add custom interface to some proto classes.(We could, just need to implement it). It would be nice to group all responses that have IsSuccess under one interface.
- Maybe remove ITask.Fail(string errorMessage) // only left IError override.
- Should IBus know about async-sync? Looks like implementation leaked into design.

## TODOs
- Check how can we back-port C# 8.0 features to .netStandar2.0
	- https://stu.dev/csharp8-doing-unsupported-things/
- Do Unit tests
- Implement simple Bus in order for tests to work
- ITask and ITask<T> Should have, same base interface (or one should inherit from another)

## Answered questions
