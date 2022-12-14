function toggleSideBar() {
    const element = document.getElementById('sideBar');
    if (element.hidden) {
        element.hidden = false;
        document.getElementById('scrim').classList.toggle('visible');
        document.getElementById('sideBarMenu').classList.toggle('slideOpen');
    }
    else {
        document.getElementById('scrim').classList.toggle('visible');
        
        document.getElementById('sideBarMenu').classList.remove('slideOpen');
        setTimeout(() => { element.hidden = true; }, 200);
    }
}