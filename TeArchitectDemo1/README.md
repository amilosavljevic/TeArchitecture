# Demo 1

	Exploring concepts like:
	- IBus for message passing (instead of events).
	- ITask to hide async commands.
	- Split models into testable IHandlers.
	- Models are data holders only. Protected mutable reference to data, public immutable. Also models can access other models data.

## Questions
- Should ITask inherit from Task(or whatever) so we can use it with await.
- Should we change model before we get response from server and do roll-back if transaction fail or should we just do entire transaction when it is approved?
- Show should we call simple message structures? I called it SellPlayerNowAction, but maybe theres a better name.
- Currently if user does not mark task as finished, it will never return.
- I would be really nice if we could add custom interface to some proto classes.(We could, just need to implement it). It would be nice to group all responses that have IsSuccess under one interface.
- Maybe remove ITask.Fail(string errorMessage) // only left IError override.
- Should IBus know about async-sync?

## TODOs
- Check how can we back-port C# 8.0 features to .netStandar2.0
	- https://stu.dev/csharp8-doing-unsupported-things/
- ITask and ITask<T> Should have, same base interface (or one should inherit from another)
