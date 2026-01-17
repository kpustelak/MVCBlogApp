function copyFunctionality(text) {
    navigator.clipboard.writeText(text);
}

    function editFileAlt(fileId, imageSrc, currentText) {
    document.getElementById('editOverlay').style.display = 'block';
    document.getElementById('editWindow').style.display = 'block';

    document.getElementById('fileId').value = fileId;
    document.getElementById('fileAltText').value = currentText || '';

    if (imageSrc) {
    document.getElementById('previewImage').src = imageSrc;
}
}

    function closeEditWindow() {
    document.getElementById('editOverlay').style.display = 'none';
    document.getElementById('editWindow').style.display = 'none';
}

    document.addEventListener('keydown', function(event) {
    if (event.key === 'Escape') {
    closeEditWindow();
}
});