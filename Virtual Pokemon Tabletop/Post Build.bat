mkdir "Building Dir\x86\Standalone\"
mkdir "Building Dir\x86_Debug\Standalone\"
xcopy /s/y "External\lua52 x32.dll" "Building Dir\x86\Standalone\lua52.dll*"
xcopy /s/y "External\lua52 x32.dll" "Building Dir\x86_Debug\Standalone\lua52.dll*"

mkdir "Building Dir\x64\Standalone\"
mkdir "Building Dir\x64_Debug\Standalone\"
xcopy /s/y "External\lua52 x64.dll" "Building Dir\x64\Standalone\lua52.dll*"
xcopy /s/y "External\lua52 x64.dll" "Building Dir\x64_Debug\Standalone\lua52.dll*"