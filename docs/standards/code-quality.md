# Code Quality Standards

## Quality Bar

All first-party code should be written as production-grade, enterprise-level code:

- High quality and predictable behavior.
- Maintainable structure with clear ownership.
- Extendable design that accommodates future gameplay growth.
- Strong adherence to SOLID principles.
- Use widely understood programming patterns when they clarify the design.
- Prefer simple, explicit code over clever code.

## SOLID Guidance

- Single Responsibility: keep systems, services, factories, and components focused on one reason to change.
- Open/Closed: extend behavior through new systems, factories, config, or strategies instead of risky rewrites.
- Liskov Substitution: keep interfaces honest; implementations should be interchangeable without hidden assumptions.
- Interface Segregation: prefer small interfaces that expose only what consumers need.
- Dependency Inversion: depend on abstractions for services and infrastructure, especially across gameplay, assets, input, time, and physics.

## Patterns

- Prefer established project patterns before introducing new ones.
- Use VContainer constructor injection for services and systems.
- Keep Entitas gameplay behavior in focused systems grouped by feature.
- Use factories for entity creation when composition is non-trivial or reused.
- Use ScriptableObjects for designer-tunable configuration when values need Unity authoring.
- Use Addressables paths consistently for asset/config loading.

## Comments And In-Code Documentation

- Add comments when intent is non-obvious, not to repeat what the code says.
- Document public abstractions when their contract is important for future extension.
- Keep comments short, factual, and close to the code they explain.
- Prefer names and small methods that reduce the need for comments.
- Add a brief comment before tricky gameplay math, lifecycle ordering, pooling behavior, or generated-code assumptions.

## Maintainability Checklist

Before finishing a code change:

- Does the change fit the existing architecture and feature boundaries?
- Is the responsibility in the right class/system/service?
- Could a future feature extend this without modifying unrelated code?
- Are dependencies injected rather than hard-coded where practical?
- Are magic values moved into config or constants when they affect tuning or behavior?
- Are generated files avoided unless regeneration is the task?
- Did documentation change if the behavior, workflow, or architecture changed?

