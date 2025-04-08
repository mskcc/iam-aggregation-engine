# ADR: Use Clean Architecture with CQRS for Separation of Concerns
This document explains why clean architecture with CQRS for SoC was chosen and why this allows for more testable, scalable, and maintainable code.  

## Decision

Implementation of a custom "mediator" class in this application to handle communication between the presentation layer and the application layer. The mediator class acts as an intermediary that facilitates the flow of data and logic between the layers.  These layers employ the clean architecture principle and contain a **presentation layer** (Api project), **Application layer** (Application project), **Domain layer** (Domain project), and the **infratructure layer** (Infrastructure project).

### Key Points of the Mediator Design:

- **Separation of Concerns**: The presentation layer (Web API) doesn't directly invoke application or domain services. Instead, it triggers a notification that is handled by the mediator. This approach allows the domain layer to remain decoupled from the presentation logic.
  
- **Simplification**: The mediator is a lightweight solution that handles the communication logic, making the system simpler to maintain. It delegates responsibilities such as accessing the domain layer to other parts of the application, which ensures that business logic remains in the domain.

- **Triggering Actions via Notifications**: The mediator listens for notifications from the presentation layer and passes those notifications to the application layer. This allows the application layer to execute domain logic without the need for the presentation layer to be coupled with ef core logic.

- **Maintainability**: With this mediator in place, adding new features or modifying the interaction between layers requires minimal changes to the existing codebase. Each layer can evolve independently, which makes for simpler development and highly testable code.

----

### Flow of Data:

1. **Presentation Layer**: The Web API layer listens for incoming HTTP requests and triggers a corresponding notification to the mediator.
   
2. **Mediator**: The custom mediator class listens for these notifications and forwards them to the appropriate **use case** (IColleague implementation) in the application layer.

3. **Application Layer**: The application layer contains the logic to interact with the domain layer. The application layer calls the domain layer for Entity Framework Core operations and returns the result.

4. **Domain Layer**: The domain layer remains independent of the presentation and application layers. It contains the core business logic and interacts with data storage through Entity Framework Core, but it is not directly accessed by the presentation layer.