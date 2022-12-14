function toggleSideBar() {
    const element = document.getElementById('sideBar');
    if (element.hidden) {
        element.hidden = false;
        document.getElementById('scrim').classList.toggle('showed');
        document.getElementById('sideBarMenu').classList.toggle('slideOpen');
        document.getElementById('sideBarMenu').classList.toggle('slide');
    }
    else {
        document.getElementById('scrim').classList.toggle('showed');
        
        document.getElementById('sideBarMenu').classList.remove('slideOpen');
        document.getElementById('sideBarMenu').classList.toggle('slide');
        setTimeout(() => { element.hidden = true; }, 200);
    }
}