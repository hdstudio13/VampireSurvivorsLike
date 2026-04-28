# Decisions

## 2026-04-28: Use `docs` as the project memory folder

Use `docs` in the project root for collaboration notes, project context, decisions, and change history.

Reason: `docs` is conventional, easy to discover, and short enough to reference frequently.

## 2026-04-28: Add local repo and Skill reminders for documentation-first work

Use both `AGENTS.md` and the local Codex Skill `.codex/skills/vampire-survivors-project` to guide future Codex sessions toward reading `docs/AI_CONTEXT.md` before inspecting source broadly.

Reason: a local Skill keeps workflow guidance versioned with the project, while `AGENTS.md` is project-local reinforcement. Together they make the desired behavior more reliable without user-wide configuration.

## 2026-04-28: Use tree-based documentation

Use `docs/AI_CONTEXT.md` as the small entry point, `docs/ARCHITECTURE.md` as the routing index, and focused branch files under `docs/architecture`, `docs/features`, `docs/systems`, and `docs/workflows`.

Reason: focused branch files reduce token use for feature tasks and make project knowledge easier to update without bloating a single document.
