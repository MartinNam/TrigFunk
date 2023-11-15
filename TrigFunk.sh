#!/bin/sh
echo -ne '\033c\033]0;TrigFunk\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/TrigFunk.x86_64" "$@"
