DesktopLazyTree
===============
WinForms\WPF приложение, в котором форма с TreeView контролом, которое отображает файловую структуру.<br/><br/>
В корневых узлах он отображает доступные диски в системе, при нажатии разверуть на конкретном, он подгружает список папок и файлов из выбранной папки, и отобразить его в дереве. При развертывании child элемента дерева, он так же должен динамически подгрузить список папок и файлов и отобразить его. Другими словами, дерево должно подгружать информацию о файлах и подпапках в конкретной папке, только тогда, когда пользователь будет раскрывать этот элемент дерева, то есть обеспечить ленивую загрузку файловой информации.
<br>Используемые технологии:
<ul>
<li>Visual Studio 2013</li>
<li>.Net/c#</li>
<li>WinForms\WPF</li> 
<li>TreeView</li>
<ul>
