# Changelog

## 2026-04-28

- Created the `docs` folder as the shared project documentation home.
- Added initial AI context, changelog, and decision-log files.
- Added `AGENTS.md` to require reading project documentation before repository tasks.
- Created the local Codex Skill `.codex/skills/vampire-survivors-project` for documentation-first project work.
- Researched the current Unity project structure and added `docs/ARCHITECTURE.md`.
- Updated `docs/AI_CONTEXT.md` with Unity version, project root, core packages, scene/build notes, and remaining unknowns.
- Verified `Jenny/Jenny-Gen.bat` can run from the `Jenny/` working directory and documented the regeneration command.
- Reworked documentation into a tree-based structure with routing index files and focused branch docs for architecture, features, systems, and workflows.
- Added `docs/workflows/export-documentation-prompt.md` to reproduce this local-only documentation workflow with another AI agent.
- Added `docs/standards/code-quality.md` for enterprise-level coding standards, SOLID guidance, patterns, and comment rules.
- Added the project-local Unity MCP package dependency `com.gamelovers.mcp-unity` and documented setup in `docs/workflows/unity-mcp.md`.
- Updated `.gitignore` to allow Unity `Packages/manifest.json` and `Packages/packages-lock.json` to be versioned.
