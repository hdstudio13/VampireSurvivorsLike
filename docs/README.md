# Project Documentation Index

This folder is the shared memory for collaboration on this project.

Keep documents short, current, and task-oriented. Read `AI_CONTEXT.md` first, then follow `ARCHITECTURE.md` to the smallest relevant branch.

## Front Door

- `AI_CONTEXT.md` - high-signal project context Codex should read before starting work.
- `ARCHITECTURE.md` - routing index for architecture, systems, feature maps, and workflows.
- `CHANGELOG.md` - concise record of meaningful project changes.
- `DECISIONS.md` - architecture and design decisions that should not be rediscovered.

## Branches

- `architecture/` - project layout, runtime boot, ECS and generated-code conventions.
- `features/` - gameplay feature maps and feature-specific notes.
- `systems/` - cross-cutting runtime systems such as input, assets/views, and physics/collision.
- `workflows/` - repeatable development workflows such as Jenny code generation and the export prompt.

## Documentation Rules

- Prefer one focused file per project area.
- Keep each file small enough to read before a feature task.
- Add links in `ARCHITECTURE.md` when adding new branch files.
- Update docs in the same task when behavior, structure, tooling, or workflow changes.
