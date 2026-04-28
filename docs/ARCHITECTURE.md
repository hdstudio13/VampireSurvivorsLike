# Architecture Index

Use this file as the navigation hub. Read only the branch that matches the task.

## Fast Routing

- Project layout, Unity version, important roots: `architecture/project-layout.md`
- Runtime boot, dependency injection, state machine: `architecture/runtime-boot.md`
- ECS conventions, Entitas contexts, generated code: `architecture/ecs-and-generated-code.md`
- Gameplay feature order and behavior map: `features/gameplay-feature-map.md`
- Entity views, Addressables, pooling, registrars: `systems/views-and-assets.md`
- Input bindings and input service: `systems/input.md`
- Physics overlap, collision registry, collision behavior: `systems/physics-and-collision.md`
- Code quality, SOLID, maintainability, comments: `standards/code-quality.md`
- Jenny/Entitas regeneration workflow: `workflows/jenny-codegen.md`
- Export prompt for reproducing this documentation workflow in another project: `workflows/export-documentation-prompt.md`

## First-Party Code Roots

- Unity project: `src/VampireSurvivorsLike`
- Main scripts: `src/VampireSurvivorsLike/Assets/Scripts`
- Gameplay features: `src/VampireSurvivorsLike/Assets/Scripts/Gameplay/Features`
- Generated Entitas code: `src/VampireSurvivorsLike/Assets/Scripts/Generated`
- Custom Jenny generators: `src/CustomGenerators`

## Documentation Rules

- Keep this file short.
- Put durable details in the most specific branch file.
- Add a new branch file when a topic grows beyond a quick summary.
- Do not duplicate source code; document intent, ownership, paths, and non-obvious behavior.
