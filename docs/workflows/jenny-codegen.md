# Jenny Code Generation

## Purpose

Jenny generates Entitas API code from component declarations and context settings.

## Configuration

- Main config: `Jenny/JennyRoslyn.properties`
- `Jenny.Plugins.TargetDirectory` writes generated output into `../src/VampireSurvivorsLike/Assets/Scripts`.
- `Jenny.Plugins.ProjectPath` points at `../src/VampireSurvivorsLike/Assembly-CSharp.csproj`.
- Configured contexts: `Game`, `Input`, `Meta`.
- Custom generators are loaded from `../src/CustomGenerators/bin`.

## Command

Run from the `Jenny/` directory:

```bat
Jenny-Gen.bat
```

The batch file invokes:

```bat
dotnet .\Jenny\Jenny.Generator.Cli.dll gen JennyRoslyn.properties -v
```

## Expected Result

- A successful run reports progress through pre-processing, model creation, file generation, post-processing, and a final generated-file count.
- Last verified result: `Generated 63 files in 31,8 seconds`.

## Notes

- The command may need permission to update `src/VampireSurvivorsLike/Assembly-CSharp.csproj`.
- After generation, inspect Git diffs. A clean generation can still print line-ending warnings for generated files.

