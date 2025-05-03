#!/bin/bash

# Find all .cs files in the Application project
find /Users/zaibimohamed/Documents/source/Mriguel/src/Core/Application -type f -name "*.cs" -print0 | while IFS= read -r -d '' file; do
    # Replace AlloVoisinClone with Mriguel in namespaces and using statements
    sed -i '' 's/AlloVoisinClone\./Mriguel\./g' "$file"
    sed -i '' 's/namespace AlloVoisinClone/namespace Mriguel/g' "$file"
    sed -i '' 's/using AlloVoisinClone/using Mriguel/g' "$file"
done

# Find all .cs files in the Persistence project
find /Users/zaibimohamed/Documents/source/Mriguel/src/Infrastructure/Persistence -type f -name "*.cs" -print0 | while IFS= read -r -d '' file; do
    # Replace AlloVoisinClone with Mriguel in namespaces and using statements
    sed -i '' 's/AlloVoisinClone\./Mriguel\./g' "$file"
    sed -i '' 's/namespace AlloVoisinClone/namespace Mriguel/g' "$file"
    sed -i '' 's/using AlloVoisinClone/using Mriguel/g' "$file"
done

# Find all .cs files in the Infrastructure project
find /Users/zaibimohamed/Documents/source/Mriguel/src/Infrastructure/Infrastructure -type f -name "*.cs" -print0 | while IFS= read -r -d '' file; do
    # Replace AlloVoisinClone with Mriguel in namespaces and using statements
    sed -i '' 's/AlloVoisinClone\./Mriguel\./g' "$file"
    sed -i '' 's/namespace AlloVoisinClone/namespace Mriguel/g' "$file"
    sed -i '' 's/using AlloVoisinClone/using Mriguel/g' "$file"
done

# Find all .cs files in the API project
find /Users/zaibimohamed/Documents/source/Mriguel/src/Presentation/API -type f -name "*.cs" -print0 | while IFS= read -r -d '' file; do
    # Replace AlloVoisinClone with Mriguel in namespaces and using statements
    sed -i '' 's/AlloVoisinClone\./Mriguel\./g' "$file"
    sed -i '' 's/namespace AlloVoisinClone/namespace Mriguel/g' "$file"
    sed -i '' 's/using AlloVoisinClone/using Mriguel/g' "$file"
done

# Find all .cs files in the SignalR project
find /Users/zaibimohamed/Documents/source/Mriguel/src/Presentation/SignalR -type f -name "*.cs" -print0 | while IFS= read -r -d '' file; do
    # Replace AlloVoisinClone with Mriguel in namespaces and using statements
    sed -i '' 's/AlloVoisinClone\./Mriguel\./g' "$file"
    sed -i '' 's/namespace AlloVoisinClone/namespace Mriguel/g' "$file"
    sed -i '' 's/using AlloVoisinClone/using Mriguel/g' "$file"
done

echo "Namespace updates completed!"
