# Export Documentation Prompt

Use this prompt with another AI coding agent when you want to recreate this project's local-only, tree-based documentation workflow in another repository.

## Prompt

```text
You are working inside an existing software project. Set up a local-only, tree-based documentation system that helps future AI agents understand the project efficiently without reading the entire source tree.

Goals:

1. Create or update a project-root `docs/` folder as the shared project memory.
2. Keep documentation local to the repository. Do not create or modify user-wide/global AI configuration.
3. Use a tree-based documentation structure:
   - `docs/AI_CONTEXT.md` as the tiny mandatory entry point.
   - `docs/README.md` as the documentation index.
   - `docs/ARCHITECTURE.md` as a routing hub, not a giant architecture dump.
   - `docs/DECISIONS.md` for durable architecture/workflow decisions.
   - `docs/CHANGELOG.md` for concise project/documentation changes.
   - `docs/architecture/` for project structure, runtime boot, architecture conventions.
   - `docs/features/` for feature-specific maps and notes.
   - `docs/systems/` for cross-cutting systems.
   - `docs/workflows/` for repeatable development workflows.
4. Add a project-root `AGENTS.md` that tells future AI agents to read docs before source:
   - Read `docs/AI_CONTEXT.md`.
   - Use `docs/ARCHITECTURE.md` as the router.
   - Open only the smallest relevant branch file.
   - Inspect source only after narrowing the search through docs.
   - Update docs in the same task when behavior, structure, tooling, or workflow changes.
5. If the environment supports local AI skills/configuration, create it inside the repository only, for example under `.codex/skills/<project-name>/`, and make it point to the same documentation-first workflow. Do not place it in a global user directory.

Research workflow:

1. Start from existing docs if any.
2. Inspect the repository shape using fast file discovery.
3. Identify project roots, primary source roots, generated/vendor/cache directories, main entry points, build/test workflows, and important tooling.
4. Read only enough source to understand durable architecture and task-routing context.
5. Document stable facts, not large code summaries.
6. Keep each branch file small and focused.

Documentation rules:

- `docs/AI_CONTEXT.md` must be short enough to read before every task.
- `docs/ARCHITECTURE.md` must route to focused files and should not become the main knowledge dump.
- Add new branch files when a topic becomes too large or independent.
- Prefer bullets with paths, ownership, and non-obvious behavior.
- Do not duplicate obvious code details.
- Do not document generated/vendor code unless future tasks need to know how to avoid or regenerate it.
- Keep `docs/CHANGELOG.md` updated when documentation structure changes.
- Keep `docs/DECISIONS.md` updated when architecture or workflow decisions are made.

Suggested initial files:

- `docs/AI_CONTEXT.md`
- `docs/README.md`
- `docs/ARCHITECTURE.md`
- `docs/DECISIONS.md`
- `docs/CHANGELOG.md`
- `docs/architecture/project-layout.md`
- `docs/architecture/runtime-boot.md`
- `docs/architecture/generated-code.md` or equivalent if generated code exists
- `docs/features/<feature-map>.md`
- `docs/systems/<system-name>.md`
- `docs/workflows/<workflow-name>.md`

Expected final result:

- Future agents can read `AI_CONTEXT.md`, use `ARCHITECTURE.md` as a router, open one or two focused branch files, then inspect only relevant source files.
- The project documentation remains local, concise, current, and optimized for repeated AI-assisted development.
```

## Local Project Variant

For this repository, the local implementation is:

- `docs/AI_CONTEXT.md`
- `docs/README.md`
- `docs/ARCHITECTURE.md`
- `docs/DECISIONS.md`
- `docs/CHANGELOG.md`
- `docs/architecture/`
- `docs/features/`
- `docs/systems/`
- `docs/workflows/`
- `AGENTS.md`
- `.codex/skills/vampire-survivors-project/`

