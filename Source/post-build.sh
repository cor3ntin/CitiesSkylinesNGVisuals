#!/bin/bash 
set -x

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
DEST="$HOME/.local/share/Colossal Order/Cities_Skylines/Addons/Mods/NGVisuals"

rm -r "$DEST" || true
mkdir -p "$DEST"


cp "$1" "$DEST"
(cd `dirname "$1"` && /opt/Unity/Editor/Data/MonoBleedingEdge/bin/mono /opt/Unity/Editor/Data/MonoBleedingEdge/lib/mono/4.5/pdb2mdb.exe `basename "$1"`)
cp "${1%.dll}.pdb" "$DEST"
cp "$1.mdb" "$DEST"
cp -r "$DIR/../Bundles" "$DEST/Resources"
