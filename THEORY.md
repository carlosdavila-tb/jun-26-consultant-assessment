## 🧠 Theory Questions

> Tick the option you believe is correct for each question. For questions marked 🔍, expand the reasoning block to explain your choice.

---

### Question 1 — Good Practices

**The `Rental` entity stores `PickupDate` and `ReturnDate` as `DateOnly` rather than `DateTime`. What does choosing `DateOnly` communicate here?**

```csharp
public required DateOnly PickupDate { get; init; }
public required DateOnly ReturnDate { get; init; }
```

- [X] A) It models a calendar date with no time-of-day, so a booking date never carries a spurious hour/minute component
- [ ] B) It makes date comparisons run measurably faster than `DateTime` at runtime
- [ ] C) It automatically converts the date to the server's local time zone on read
- [ ] D) It is required because ASP.NET cannot serialize `DateTime` to JSON

---

### Question 2 — Good Practices

**`Rental`'s properties use `get; init;`. What does declaring them init-only mean for a booked rental?**

```csharp
public required string CustomerName { get; init; }
public required decimal TotalCost { get; init; }
```

- [ ] A) The properties can be reassigned at any time after the object is created
- [X] B) The properties can be set only while the object is being constructed, so a stored rental's fields can't be altered afterward
- [ ] C) The properties are ignored during JSON serialization
- [ ] D) The properties must all be value types

---

### Question 3 — Good Practices

**The confirmation line is built with an interpolated string. Which statement about this is true?**

```csharp
string summary = $"EMAIL -> {rental.CustomerName}: Your {rental.VehicleClass} is booked for {rental.Days} day(s).";
```

- [ ] A) Interpolation changes the value of `rental.CustomerName`
- [ ] B) Interpolation only works inside controller actions
- [ ] C) The `$"..."` form embeds the values inline, which reads more clearly than joining fragments with `+`
- [ ] D) Interpolation rounds any numeric value to two decimals automatically

---

### Question 4 — Good Practices

**`BookRental` rejects each invalid field with an early `throw` before the main work begins. What does this early-return style buy the reader?**

```csharp
if (string.IsNullOrWhiteSpace(request.CustomerName))
{
    throw new ArgumentException("Customer name is required.");
}
// ... the main path continues, un-nested, below
```

- [ ] A) It makes the method run on a background thread
- [ ] B) It guarantees the method never throws
- [ ] C) It removes the need to test the invalid cases
- [X] D) It rejects bad input up front, so the successful path reads top-to-bottom without deep nesting

---

### Question 5 — Good Practices

**The service's private fields are named `_rentals` and `_confirmations`. Which statement about this naming is true in this codebase?**

- [X] A) The leading underscore + camelCase marks them as private fields, matching the convention used across the project
- [ ] B) The leading underscore makes the fields publicly accessible
- [ ] C) The underscore is required for the garbage collector to track them
- [ ] D) The names should be PascalCase like `Rentals` to follow the convention

---

### Question 6 — Good Practices

**Validation uses `string.IsNullOrWhiteSpace(request.CustomerName)` rather than `request.CustomerName == null`. What does this guard against that a plain null check would not?**

- [ ] A) Nothing — the two checks are equivalent
- [X] B) It also rejects empty or whitespace-only names, not just a missing (null) value
- [ ] C) It converts the name to upper case before storing
- [ ] D) It prevents the name from ever being null at compile time

---

### Question 7 — Good Practices

**Compare the method name `BookRental` with an alternative name `Handle`. Which statement best reflects good naming here?**

- [ ] A) `Handle` is better because shorter names compile faster
- [ ] B) Both are equally clear since the class name already mentions "Rental"
- [X] C) `BookRental` states the action the method performs, so a caller understands it without opening the body
- [ ] D) Method names should be nouns, so `Rental` would be the best choice

---

### Question 8 — Good Practices

**The startup seed builds three rentals by repeating the same `CreateRentalRequest` construction with different values. A teammate wants to reduce the repetition. Which approach best applies here?**

```csharp
rentalService.BookRental(new CreateRentalRequest { CustomerName = "Dana...", VehicleClass = "Compact", /* ... */ });
rentalService.BookRental(new CreateRentalRequest { CustomerName = "Marcus...", VehicleClass = "SUV", /* ... */ });
```

- [ ] A) Leave it as-is — seed code never needs to be clean
- [X] B) Add a comment above each block explaining the fields
- [ ] C) Copy the block a fourth time so all the seeds look symmetric
- [ ] D) Extract a small helper that builds a request from the varying values and call it for each seed

---

### Question 9 — Good Practices

**The total currently appears only inside the pre-formatted confirmation text (`{rental.TotalCost:C}`). The branch now serves multiple currencies and wants the raw amount available to other parts of the system. Which approach best supports that?**

- [X] A) Keep the numeric total available as data on the rental and format it for presentation at the edge, rather than only embedding it in a formatted message
- [ ] B) Store the total only as the formatted string and re-parse the number when it is needed
- [ ] C) Round the total to two decimals before storing so the string is always correct
- [ ] D) Remove the total from the confirmation entirely so there is one less place to maintain

---

### Question 10 — Good Practices

**`GetRentals` returns `IReadOnlyList<Rental>` rather than `List<Rental>`. What does the read-only return type communicate to a caller?**

- [ ] A) The returned collection is a fresh copy on every call
- [X] B) The caller is meant to read the collection, not mutate it; the service owns the underlying list
- [ ] C) The collection can hold at most a fixed number of items
- [ ] D) The caller must release the collection back to the service after use

---

### Question 11 — Good Practices

**`Rental` marks its properties `required`. What does `required` enforce?**

- [ ] A) The properties are validated against a database on save
- [ ] B) The properties accept null values by default
- [X] C) An instance can't be constructed without those properties set, so a `Rental` missing its customer or dates can't exist
- [ ] D) The properties become read-only constants shared by all instances

---

### Question 12 — Good Practices

**The day/total calculation depends only on the request's dates and rate — no fields, no clock, no I/O. Which statement about such a calculation is most accurate?**

```csharp
int days = request.ReturnDate.DayNumber - request.PickupDate.DayNumber;
decimal totalCost = days * request.DailyRate;
```

- [ ] A) It must be moved into the controller to be testable
- [X] B) It can only be verified through an end-to-end test
- [ ] C) Its result changes depending on how many times it is called
- [ ] D) Because it depends only on its inputs, the same inputs always produce the same result, making it easy to test in isolation

---

### Question 13 — Good Practices

**`BookRental` signals invalid input by throwing `ArgumentException` with a message. Compared with returning `null` or a bare `false`, what does throwing a specific exception communicate?**

- [X] A) The failure and its reason are explicit and can't be silently ignored by the caller
- [ ] B) The method will automatically retry the booking
- [ ] C) The caller no longer needs to handle the error
- [ ] D) The exception makes the method run asynchronously

---

### Question 14 — Good Practices

**The confirmation text repeats values that are already on the `rental` object (customer, vehicle, days, total). A reviewer worries the message and the stored rental could drift apart. Which approach best prevents that?**

```csharp
var rental = new Rental { /* ... */ Days = days, TotalCost = totalCost };
_rentals.Add(rental);
string summary = $"EMAIL -> {rental.CustomerName}: ... {rental.Days} day(s) ... {rental.TotalCost:C}.";
```

- [ ] A) Build the message from the original request values instead of the rental
- [X] B) Build the message from the `rental` object's own fields, so the message and the record always reflect the same data
- [ ] C) Store the message and the rental in the same list to keep them together
- [ ] D) Add a comment reminding future editors to keep them in sync

---

### Question 15 — Good Practices 🔍

**Invalid booking input is currently surfaced by throwing `ArgumentException`, which the controller catches. The team debates whether expected validation failures should be exceptions at all. Which approach would you choose, and why?**

```csharp
// Current: throw on invalid input
if (request.DailyRate <= 0) throw new ArgumentException("Daily rate must be greater than zero.");

// Alternative: return an outcome the caller must inspect
// var result = _service.BookRental(request); if (!result.Succeeded) { ... }
```

- [ ] A) Remove the validation checks and let invalid data fail later when it is used
- [ ] B) Log a warning and continue booking with the invalid values
- [X] C) Keep throwing for invalid input — the success path stays clean and the failure can't be silently ignored
- [ ] D) Return a result/outcome object the caller must inspect — an expected invalid booking becomes a normal return value rather than control flow via an exception

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 16 — Architectural Patterns

**`Rental` references no ASP.NET or Swagger types — it is plain data. What does keeping the model free of framework types buy?**

- [ ] A) It makes JSON serialization impossible
- [X] B) It forces every controller to share one model
- [ ] C) It prevents the model from being held in a list
- [ ] D) The domain entity stays independent of the web layer, so it can be used and tested without involving HTTP

---

### Question 17 — Architectural Patterns

**The project separates `Controllers/`, `Services/`, and `Models/`. A new rule for computing a loyalty discount on the total belongs in which place?**

- [ ] A) The service, where business logic lives
- [X] B) The controller action, next to the HTTP handling
- [ ] C) The `Rental` model's property getter
- [ ] D) `Program.cs`, alongside the startup wiring

---

### Question 18 — Architectural Patterns

**`RentalsController` depends on `IRentalService`, not the concrete `RentalService`. What does depending on the interface allow?**

- [ ] A) It lets the controller call the data store directly
- [X] B) The implementation behind the interface can be swapped or substituted (for example, with a test stand-in) without changing the controller
- [ ] C) It removes the need to register the service at startup
- [ ] D) It makes the controller run before the service is built

---

### Question 19 — Architectural Patterns

**`CreateRentalRequest` (the POST input) is a separate type from the stored `Rental`. What does keeping them separate reflect?**

- [ ] A) The two types must always expose identical fields
- [ ] B) `CreateRentalRequest` exists only because a controller cannot accept a class
- [ ] C) The shape clients send is decoupled from the stored entity, so one can change without forcing the other
- [ ] D) Splitting them makes the in-memory lookup faster

---

### Question 20 — Architectural Patterns

**Trace what happens when a client calls `POST /rentals`. Which describes the flow?**

- [ ] A) The model validates the request, then calls the controller
- [ ] B) The controller writes to the data store directly and skips the service
- [ ] C) `Program.cs` handles the request and returns the response
- [ ] D) The controller receives the request, delegates to the service (which holds the booking logic), and returns the service's result as the response

---

### Question 21 — Architectural Patterns

**`GetRentals` returns the entire list every time. As Riverbend's history grows into the thousands of rentals, which change best fits the layered design?**

- [ ] A) Return a bounded page of rentals from the service/endpoint (for example, a page index plus size), instead of always loading everything
- [ ] B) Move the full list into the controller so it loads faster
- [ ] C) Cache the full list inside the `Rental` model
- [ ] D) Return the rentals as one concatenated string to shrink the response

---

### Question 22 — Architectural Patterns

**Suppose Riverbend opens a second branch and the two share one bookings store across regions. During a network partition between regions, what does the CAP theorem say the system must trade off?**

- [ ] A) It can keep full consistency, availability, and partition tolerance all at once
- [ ] B) Under a partition it must choose between staying consistent (rejecting some requests) and staying available (risking divergent data)
- [ ] C) Partitions only affect read performance, never correctness
- [ ] D) Adding more regions removes the trade-off entirely

---

### Question 23 — Architectural Patterns

**Because `Rental` and the booking logic don't depend on ASP.NET, what is protected if the team later swapped the web framework?**

- [ ] A) Nothing — swapping the framework always rewrites the domain logic
- [ ] B) The database schema is migrated automatically
- [ ] C) The business rules and entity don't change, because they don't depend on the transport layer
- [ ] D) The JSON wire format is guaranteed to stay identical

---

### Question 24 — Architectural Patterns

**The team plans to replace the in-memory list with a real database later. Which approach keeps `BookRental`'s logic unaffected when storage changes?**

- [ ] A) Inline the database calls directly inside `BookRental`
- [ ] B) Move the booking logic into the controller so the service can be deleted
- [ ] C) Have the controller talk to the database and bypass the service
- [ ] D) Have the service depend on a storage interface, with the in-memory and database versions as interchangeable implementations

---

### Question 25 — Architectural Patterns 🔍

**Riverbend wants bookings to survive an application restart (they are in-memory today). Which persistence approach would you choose, and why?**

- [ ] A) Put the store behind a data-access interface backed by a managed relational database — durable and queryable, at the cost of an external dependency and its connection/operational concerns
- [ ] B) Keep an in-process store but periodically write a snapshot to a local file that the app reloads on start — fewer moving parts, but weaker concurrency and query support
- [ ] C) Leave it in memory and ask staff to re-enter bookings after a restart
- [ ] D) Write every booking to a plain log line printed to the console

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 26 — Backend Frameworks

**`RentalsController` is annotated `[ApiController]`, `[Route("rentals")]`, with `[HttpGet]`, `[HttpGet("confirmations")]`, and `[HttpPost]`. What do these attributes do?**

- [ ] A) They register the service in the DI container
- [ ] B) They map HTTP verbs and paths to action methods, so `GET /rentals` reaches `GetRentals` and `GET /rentals/confirmations` reaches `GetConfirmations`
- [ ] C) They validate the request body before the action runs
- [ ] D) They cache the responses for all GET endpoints

---

### Question 27 — Backend Frameworks

**`BookRental([FromBody] CreateRentalRequest request)` — what does `[FromBody]` do?**

- [ ] A) It reads the value from the URL query string
- [ ] B) It validates that the rate is positive
- [ ] C) It binds (deserializes) the JSON request body into the `CreateRentalRequest` object
- [ ] D) It sets the HTTP response status code

---

### Question 28 — Backend Frameworks

**`Program.cs` registers `AddSingleton<IRentalService, RentalService>()`. What does the singleton lifetime mean here?**

```csharp
builder.Services.AddSingleton<IRentalService, RentalService>();
```

- [ ] A) A new `RentalService` is created for every HTTP request
- [ ] B) A new `RentalService` is created at every injection point
- [ ] C) The service is created only when first booked and discarded after each request
- [ ] D) One instance lives for the application's lifetime and is shared across all requests, which is why the in-memory rentals persist between calls

---

### Question 29 — Backend Frameworks

**When `BookRental` throws `ArgumentException` for invalid input, the controller catches it and returns `BadRequest`. A teammate suggests letting it bubble up as a 500 instead. Which best handles a predictable invalid-input case?**

- [ ] A) Translate the invalid input to a 400 (BadRequest) at the controller so clients get a clear client-error response
- [ ] B) Let it become a 500 so the client knows something went wrong
- [ ] C) Swallow the exception and return 200 with an empty body
- [ ] D) Retry the booking automatically until it succeeds

---

### Question 30 — Backend Frameworks

**The service methods are synchronous, which is fine for an in-memory list. Which statement is true about introducing `async`/`await` once a real database is added?**

- [ ] A) `async` speeds up pure in-memory work as well, so it should be added now
- [ ] B) `async`/`await` frees the request thread during I/O waits, improving throughput under load; for pure in-memory work it adds no benefit
- [ ] C) `async` is required for any method that returns a value
- [ ] D) `async` changes the HTTP status codes the action returns

---

### Question 31 — Backend Frameworks

**The in-memory rentals reset on restart. The team wants bookings to survive restarts using a real store, with the least disruption to the existing code. Which approach best fits the framework?**

- [ ] A) Instantiate the database client with `new` inside `BookRental`
- [ ] B) Move all booking logic into `Program.cs`
- [ ] C) Register a persistent implementation behind `IRentalService` (or its storage interface) via DI, swapping the in-memory one without changing the controller
- [ ] D) Have the controller open its own database connection per request

---

### Question 32 — Backend Frameworks 🔍

**Given the in-memory design, how would you register the rental store's lifetime in DI, and why?**

```csharp
// Current:
builder.Services.AddSingleton<IRentalService, RentalService>();
```

- [ ] A) Register it transient so a new instance is created at every injection point
- [ ] B) Register it however the template defaults, without considering lifetime
- [ ] C) Keep it singleton — one shared instance keeps the in-memory bookings across requests, but the shared mutable state must be made safe under concurrency
- [ ] D) Switch to scoped — each request gets a fresh instance with simpler isolation, but in-memory bookings won't persist between requests unless storage moves elsewhere

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 33 — Agentic AI

**Riverbend pilots a front-desk assistant that drafts customer confirmation messages, which should read consistently. What does lowering the model's temperature do?**

- [ ] A) It reduces randomness in the output, producing more consistent, repeatable wording
- [ ] B) It increases the size of the context window
- [ ] C) It makes the model respond faster
- [ ] D) It guarantees the output is factually correct

---

### Question 34 — Agentic AI

**The assistant is given a long list of current rentals plus the conversation so far. What does the model's context window limit?**

- [ ] A) The number of customers the branch can have
- [ ] B) The total tokens (prompt plus response) the model can consider at once, so oversized inputs must be trimmed or summarized
- [ ] C) How many times per day the assistant can be called
- [ ] D) The maximum value the temperature setting can take

---

### Question 35 — Agentic AI

**The assistant's system prompt says: "You are Riverbend's front-desk assistant. Answer only questions about vehicle rentals; decline anything else." What does this role instruction primarily shape?**

- [ ] A) The data the model was trained on
- [ ] B) The hardware the model runs on
- [ ] C) The tone, scope, and format of the assistant's responses
- [ ] D) The network latency of each call

---

### Question 36 — Agentic AI

**For a multi-step question ("which rentals are overdue, and by how many days"), the prompt asks the model to work through the steps before giving the answer. Which technique is this?**

- [ ] A) Lowering the temperature to zero
- [ ] B) Few-shot labeling
- [ ] C) Shrinking the context window
- [ ] D) Chain-of-thought prompting — guiding step-by-step reasoning to improve accuracy on multi-step tasks

---

### Question 37 — Agentic AI

**The assistant sometimes states a daily rate that isn't in Riverbend's data. Which approach best reduces this?**

- [ ] A) Provide the actual rate data as grounded context and instruct the assistant to answer only from it, not from memory
- [ ] B) Raise the temperature so the answers sound more confident
- [ ] C) Add more unrelated examples to the prompt
- [ ] D) Ask the same question repeatedly until the answers agree

---

### Question 38 — Agentic AI

**The assistant keeps the entire conversation and the full rentals list in context on every turn. Which statement about this unbounded context growth is true?**

- [ ] A) It has no downside because larger context is always better
- [ ] B) Tokens and cost grow, and irrelevant content can crowd out what matters, so the context needs deliberate pruning or summarizing
- [ ] C) It permanently increases the model's maximum context window
- [ ] D) It guarantees the assistant never repeats itself

---

### Question 39 — Agentic AI

**The team edits the confirmation-drafting prompt often. One change quietly degraded quality and there was no way to go back. Which approach best addresses this?**

- [ ] A) Keep only the latest prompt text in a code comment
- [ ] B) Avoid changing the prompt once it works
- [ ] C) Keep prompts under version control so changes are tracked and earlier versions can be restored
- [ ] D) Raise the temperature to mask the regression

---

### Question 40 — Agentic AI 🔍

**The assistant drafts customer-facing confirmation messages. How would you deploy it, and why?**

- [ ] A) Let the model send the confirmations directly with temperature set high for variety
- [ ] B) Skip any review and fix issues only when customers complain
- [ ] C) Route every draft through a human reviewer before it is sent — maximizes brand and accuracy control, at the cost of speed and staff time
- [ ] D) Send automatically with guardrails plus sampled post-hoc review — maximizes speed and scale while accepting a monitored level of risk

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 41 — CI/CD

**The rental-desk change is delivered on a feature branch via a pull request rather than pushed straight to `main`. What does the PR-based flow provide?**

- [ ] A) Review and automated checks run before the change merges, keeping `main` releasable
- [ ] B) It makes the build skip the test stage
- [ ] C) It deploys directly to production on push
- [ ] D) It removes the need for version control

---

### Question 42 — CI/CD

**A pipeline runs build → unit tests → deploy-to-dev. The unit tests fail. What should happen?**

- [ ] A) Deploy anyway and fix the tests later
- [ ] B) The pipeline stops and does not deploy — a failed test gate blocks promotion
- [ ] C) Skip the failing tests so the build goes green
- [ ] D) Roll back the previous production release

---

### Question 43 — CI/CD

**The team adds a dependency vulnerability scan as a pipeline stage before deployment. What does running it there achieve?**

- [ ] A) It replaces the need for unit tests
- [ ] B) It speeds up the build
- [ ] C) It catches known-vulnerable packages before they reach production, instead of discovering them after release
- [ ] D) It deploys the application to every environment at once

---

### Question 44 — Frontend Frameworks

**`RentalListComponent`'s template uses an `@empty` block inside `@for`. When does it render?**

```html
@for (rental of rentals; track rental.id) {
  <tr>...</tr>
} @empty {
  <tr><td>No rentals to show.</td></tr>
}
```

- [ ] A) On every change-detection cycle
- [ ] B) Only when the page first loads
- [ ] C) When the rentals request fails
- [ ] D) When the `rentals` collection is empty

---

### Question 45 — Frontend Frameworks

**`RentalListComponent` loads its data in `ngOnInit` rather than in the constructor. What does `ngOnInit` give that the constructor does not?**

- [ ] A) It runs after the component's bindings are initialized, which is the appropriate point to do initialization work like loading data
- [ ] B) It runs before the class is constructed
- [ ] C) It guarantees the HTTP call is synchronous
- [ ] D) It prevents the component from ever re-rendering

---

### Question 46 — Frontend Frameworks

**The components are standalone and list `imports: [CommonModule]`. What does the `imports` array do for a standalone component?**

- [ ] A) It registers the component as a global singleton
- [ ] B) It makes the listed directives and pipes available to the component's template without an NgModule
- [ ] C) It imports the backend API definitions
- [ ] D) It lazy-loads the component on first render

---

### Question 47 — Frontend Frameworks

**The dashboard shows raw ISO date strings for pickup and return. Staff want them shown in a readable local format. Which approach best fits Angular?**

- [ ] A) Manually slice the ISO string in the component with substring math
- [ ] B) Store a second pre-formatted date field on the backend
- [ ] C) Use the framework's date pipe in the template to format the value for presentation
- [ ] D) Convert the date to a number and show that

---

### Question 48 — Frontend Frameworks

**Suppose the rentals observable from `RentalService` were bound in the template with the async pipe. What does the async pipe manage?**

- [ ] A) It converts the observable into a Promise
- [ ] B) It caches the data on the server
- [ ] C) It increases the polling frequency of the request
- [ ] D) It subscribes and unsubscribes automatically and renders emitted values, avoiding a manual subscription leak

---

### Question 49 — Frontend Frameworks

**The service is typed `getRentals(): Observable<Rental[]>` using the `Rental` interface. The API will soon add a field. Which approach best preserves type safety across the components that use it?**

- [ ] A) Add the new field to the `Rental` interface so the compiler tracks its use everywhere, rather than switching to `any`
- [ ] B) Type the response as `any` so no changes are needed
- [ ] C) Cast the response to `unknown` and read fields by string
- [ ] D) Duplicate the `Rental` interface inside each component

---

### Question 50 — Frontend Frameworks 🔍

**The per-row total needs to be shown as currency. How would you produce the formatted value, and why?**

- [ ] A) Hard-code a "$" in front of the raw number directly in the template
- [ ] B) Format the amount with the framework's currency pipe in the template — declarative and locale-aware, but the formatting lives in the view
- [ ] C) Precompute a formatted total string in the component (for example, a `formattedTotal` field) — easier to unit-test and control, but the component now owns a presentation detail
- [ ] D) Store the formatted string on the server and never format on the client

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>

---

### Question 51 — Infrastructure

**The team weighs hosting the .NET API on a managed application platform (PaaS) versus a bare virtual machine (IaaS). What does the PaaS option manage that the IaaS option leaves to the team?**

- [ ] A) The application's business logic
- [ ] B) The customers' booking data
- [ ] C) The operating system, runtime, patching, and scaling — the team deploys the app rather than maintaining the server
- [ ] D) The HTTP status codes the API returns

---

### Question 52 — Infrastructure

**When choosing a cloud region to host the API, what does picking a region close to Riverbend's customers primarily affect?**

- [ ] A) The programming language the API must use
- [ ] B) The number of endpoints the API can expose
- [ ] C) The version of .NET available
- [ ] D) Network latency for users (and where the data physically resides)

---

### Question 53 — Infrastructure

**The Angular app calls the backend. Which statement is true about serving that traffic over HTTPS/TLS rather than plain HTTP?**

- [ ] A) TLS encrypts the data in transit, so the request contents aren't readable by someone observing the network
- [ ] B) TLS makes the responses render faster in the browser
- [ ] C) TLS removes the need for any CORS configuration
- [ ] D) TLS stores the data encrypted in the database

---

### Question 54 — TDD

**Which structure best describes a unit test for `BookRental`'s calculation?**

- [ ] A) Assert first, then arrange, then act
- [ ] B) Arrange (build a `CreateRentalRequest`), act (call `BookRental`), assert (check the returned rental's days and total)
- [ ] C) Call the endpoint over HTTP, then read the database, then print the result
- [ ] D) One line that constructs, calls, and asserts together for brevity

---

### Question 55 — TDD

**A test asserts that for a 3-day rental at 42.00/day, `BookRental` returns a rental whose `TotalCost` is 126.00. What does this assertion verify?**

- [ ] A) That `BookRental` calls its internal list exactly once
- [ ] B) That the method runs in under a millisecond
- [ ] C) The observable behavior — the output produced for given inputs, regardless of how it is computed
- [ ] D) That the controller returns a 201 status

---

### Question 56 — TDD

**In the red/green/refactor cycle, what must remain true during the refactor step?**

- [ ] A) The tests are deleted and rewritten from scratch
- [ ] B) New behavior is added in the same step
- [ ] C) The production code is left unchanged
- [ ] D) Behavior stays the same and the existing tests still pass while the code's structure is improved

---

### Question 57 — TDD

**Which set of cases best covers `BookRental`'s input validation?**

- [ ] A) Empty customer name, empty vehicle class, return date not after pickup, non-positive rate, plus one fully valid booking — covering the empty, exception, boundary, and simple cases
- [ ] B) Only one valid booking with typical values
- [ ] C) Many random valid bookings and nothing else
- [ ] D) Only the case where the rate is exactly zero

---

### Question 58 — TDD

**Every test rebuilds the same `CreateRentalRequest` before calling `BookRental`. What does extracting a shared builder or helper for it achieve?**

- [ ] A) It makes the tests depend on each other's order
- [ ] B) It removes the duplication, so a change to the request shape updates one place and each test stays focused on its scenario
- [ ] C) It turns the tests into integration tests
- [ ] D) It guarantees 100% code coverage

---

### Question 59 — TDD

**The day/total calculation should be checked across several (days, rate, expected total) combinations. Which approach verifies them with the least duplication?**

- [ ] A) Copy the same test method once per combination
- [ ] B) Assert only the first combination and assume the rest
- [ ] C) Use a parameterized test (`[Theory]` with `[InlineData]` rows) feeding each combination into one test
- [ ] D) Test the combinations only through the live HTTP endpoint

---

### Question 60 — TDD 🔍

**The team must decide how to balance end-to-end tests (driving the Angular page against the live API) against tests below the UI. Which emphasis would you choose, and why?**

- [ ] A) Test only by manually clicking through the page before each release
- [ ] B) Write no tests below the UI, since the end-to-end tests cover everything
- [ ] C) Invest mainly in end-to-end tests that drive the page against the live API — highest whole-stack confidence, at the cost of slow, brittle, hard-to-pinpoint feedback
- [ ] D) Invest mainly in fast unit/integration tests below the UI with a few end-to-end checks — fast, precise feedback, at the cost of possibly missing some integration gaps

<details open>
<summary>💬 Your reasoning</summary>

_Explain why you chose your answer..._

</details>
