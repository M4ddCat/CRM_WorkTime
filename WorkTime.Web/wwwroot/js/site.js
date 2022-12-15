function toggleSideBar() {
    const element = document.getElementById('sideBar'),
        scrim = document.getElementById('scrim'),
        menu = document.getElementById('sideBarMenu');
    
    if (element.classList.contains('hidden')) {
        element.classList.toggle('hidden');
        setTimeout(() => {
        scrim.classList.toggle('showed');
        menu.classList.toggle('show'); }, 1);
    }
    else {
        scrim.classList.toggle('showed');
        menu.classList.toggle('show');
        setTimeout(() => { element.classList.toggle('hidden'); }, 200);
    }
}