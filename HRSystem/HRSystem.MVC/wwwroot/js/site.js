// ─── Sidebar Toggle ───
const sidebar = document.getElementById('sidebar');
const mainContent = document.getElementById('mainContent');
const overlay = document.getElementById('sidebarOverlay');
const toggleBtn = document.getElementById('sidebarToggle');

toggleBtn.addEventListener('click', () => {
    const collapsed = sidebar.classList.toggle('collapsed');
    mainContent.classList.toggle('expanded', collapsed);
    // On mobile show overlay
    if (window.innerWidth < 768) {
        overlay.style.display = collapsed ? 'none' : 'block';
    }
});

overlay.addEventListener('click', () => {
    sidebar.classList.add('collapsed');
    mainContent.classList.add('expanded');
    overlay.style.display = 'none';
});

// Responsive: collapse sidebar on small screens by default
function checkResize() {
    if (window.innerWidth < 768) {
        sidebar.classList.add('collapsed');
        mainContent.classList.add('expanded');
    } else {
        sidebar.classList.remove('collapsed');
        mainContent.classList.remove('expanded');
        overlay.style.display = 'none';
    }
}
window.addEventListener('resize', checkResize);
checkResize();