# Unity MCP

## Package

- Package: `com.gamelovers.mcp-unity`
- Source: `https://github.com/CoderGamester/mcp-unity.git#1.2.0`
- Installed locally through `src/VampireSurvivorsLike/Packages/manifest.json`.
- This is a project-local Unity Package Manager dependency; no global MCP client config was written.

## Why This Server

- Supports Unity 6+.
- Exposes Unity Editor tools and resources through MCP.
- Documents compatibility with Codex CLI and other MCP clients.
- Uses a Unity Editor package plus a Node.js MCP bridge.

## Requirements

- Unity 6 or later.
- Node.js 18 or later available to Unity/the MCP bridge.
- npm 9 or later if the server needs manual install/debug.

The Codex bundled Node runtime is available at:

```text
C:\Users\Admin\.cache\codex-runtimes\codex-primary-runtime\dependencies\node\bin\node.exe
```

It reports Node `v24.14.0`, which satisfies the Node version requirement. System `npm` was not available on PATH during setup, so Unity may need a normal Node/npm installation or explicit npm path configuration if the package cannot install its server dependencies automatically.

## Unity Editor Setup

After opening the Unity project:

1. Let Unity resolve the new package dependency.
2. Open `Tools > MCP Unity > Server Window`.
3. Use the window to install/configure the server if prompted.
4. Click `Start Server` to start the Unity-side WebSocket server.
5. Keep the Unity Editor open while using MCP tools.

Default WebSocket port is `8090`.

## MCP Client Configuration

The upstream manual Codex CLI configuration is:

```toml
[mcp_servers.mcp-unity]
command = "node"
args = ["ABSOLUTE/PATH/TO/mcp-unity/Server~/build/index.js"]
```

For this project, keep MCP client configuration local where possible. Do not write to user-wide/global Codex config unless explicitly requested.

The actual package cache path is resolved by Unity after package installation. Look for the package under the Unity project `Library/PackageCache` after Unity finishes resolving packages.

## Verification Notes

- `manifest.json` has been updated.
- `packages-lock.json` is expected to update after Unity resolves the git package.
- A full MCP smoke test requires opening Unity, starting `Tools > MCP Unity > Server Window`, and connecting an MCP-capable client.

