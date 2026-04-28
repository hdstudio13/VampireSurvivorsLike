# AI Context

Read this file first before starting project tasks.

## Project Snapshot

- Project: Vampire Survivors-like Unity game.
- Root: `D:\Unity\MyProjects\VampireSurvivorsLike`
- Unity project root: `src/VampireSurvivorsLike`
- Unity version: `6000.3.12f1`
- Main first-party code: `src/VampireSurvivorsLike/Assets/Scripts`
- Documentation router: see `docs/ARCHITECTURE.md`, then read only the relevant branch file.
- Main scene: `Assets/Scenes/GameplayScene.unity`
- Build settings include only `GameplayScene`.
- Main packages: VContainer, Entitas/Jenny, UniTask, Addressables, Unity Input System, Cinemachine, URP, DOTween, NaughtyAttributes.
- Third-party/vendor-heavy areas: `Assets/Entitas`, `Assets/JMO Assets`, `Assets/Plugins`, Unity `Library`.

## Working Agreement

- Use this `docs` folder as the first place to look for project context.
- `AGENTS.md` in the repository root reinforces this workflow for Codex.
- The local Codex Skill at `.codex/skills/vampire-survivors-project` also points future project work back to these docs.
- Use tree-based documentation: read this file, use `docs/ARCHITECTURE.md` as the router, then open only the relevant branch under `architecture/`, `features/`, `systems/`, or `workflows/`.
- Keep updates concise and factual.
- When a task changes behavior, structure, tooling, or workflow, update the relevant documentation in the same change.
- Prefer targeted source inspection based on this context instead of reading the whole project from scratch.

## Current Unknowns

- Automated build workflow.
- Automated test workflow; no first-party test folders were found during the initial research pass.
- Exact scene object wiring beyond `GameplayScene`, `RootLifetimeScope`, and `GameplayLifetimeScope` prefabs.
