$fws = "net461;netstandard1.4;netcoreapp2.1;netcoreapp3.1;net5.0".Split(";")
$content = "<Project>`n"
Get-ChildItem ../Newbe.ObjectVisitor.XmlDocuments -Directory | ForEach-Object {
    $lang = $_.Name
    $fws | ForEach-Object {
        $fw = $_
        $content += @"
    <ItemGroup>
        <Content Include="`$(SolutionDir)/Newbe.ObjectVisitor/Newbe.ObjectVisitor.XmlDocuments/$lang/Newbe.ObjectVisitor.xml" Link="Localization/$fw/$lang/Newbe.ObjectVisitor.xml" Pack="true" PackagePath="lib/$fw/$lang/Newbe.ObjectVisitor.xml">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
        </Content>
    </ItemGroup>

"@
    }
}
$content += "</Project>"
$content | Out-File LocalizationXml.props -Encoding utf8NoBOM