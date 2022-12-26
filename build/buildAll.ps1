pushd .
try
{
    cd ..
    
    dotnet build
    if($? -eq 0)
    {
        throw "Build failed."
    }

    dotnet test
    if($? -eq 0)
    {
        throw "Tests failed."
    }
}
finally
{
    popd
}
