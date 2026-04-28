---
name: vampire-survivors-project
description: Local project workflow for the VampireSurvivorsLike Unity game at D:\Unity\MyProjects\VampireSurvivorsLike. Use before any task in this repository, including code edits, architecture investigation, debugging, Unity gameplay changes, documentation updates, changelog updates, or planning work, so Codex reads the project docs first and keeps them current.
---

# Vampire Survivors Project

## Overview

Use this local skill to work efficiently in the VampireSurvivorsLike Unity project without rediscovering project context from scratch. Treat the repository `docs/` folder as a tree-based project memory and update the smallest relevant branch whenever work changes meaningful context.

## Start Every Task

1. Open `D:\Unity\MyProjects\VampireSurvivorsLike\docs\AI_CONTEXT.md`.
2. Open `D:\Unity\MyProjects\VampireSurvivorsLike\docs\ARCHITECTURE.md` as the routing index.
3. Open only the smallest relevant branch file under `docs\architecture`, `docs\features`, `docs\systems`, or `docs\workflows`.
4. Open `docs\README.md` only if the documentation structure itself is unclear.
5. Open `docs\DECISIONS.md` before changing architecture, conventions, workflows, or cross-cutting behavior.
6. Open `docs\standards\code-quality.md` before implementing or reviewing first-party code.
7. Open `docs\CHANGELOG.md` before summarizing recent project changes or adding new change notes.
8. Use the docs to narrow source inspection. Prefer targeted searches over broad project reads.

## During Work

- Respect current repository patterns before adding new abstractions.
- Write enterprise-level code: maintainable, extendable, SOLID, and aligned with widely understood patterns.
- Add concise comments or public contract documentation when intent, lifecycle, or extension rules are non-obvious.
- Record durable high-level discoveries in `docs\AI_CONTEXT.md` only when they affect most future tasks.
- Record area-specific discoveries in the matching branch file.
- Record durable architecture or workflow decisions in `docs\DECISIONS.md`.
- Record meaningful user-visible, gameplay, tooling, or documentation changes in `docs\CHANGELOG.md`.
- Keep documentation short and scannable. Prefer bullets and factual notes over long explanations.

## Before Finishing

1. Check whether the task changed project knowledge that future work should know.
2. Update `docs\` in the same change when it did.
3. Mention documentation updates in the final response.

## Documentation Boundaries

- Do not paste large source summaries into docs.
- Do not duplicate obvious code details that can be found with a targeted search.
- Prefer documenting stable context: architecture, conventions, workflows, decisions, and non-obvious discoveries.
- Add a new branch file when a topic grows beyond a quick summary, and link it from `docs\ARCHITECTURE.md`.
