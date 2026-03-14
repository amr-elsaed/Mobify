/* ═══════════════════════════════════════════════════════════
   MOBIFY — Main JavaScript
   Interactive features for the Mobify website layout
   ═══════════════════════════════════════════════════════════ */

document.addEventListener('DOMContentLoaded', () => {
    'use strict';

    // ─── DOM Elements ───
    const siteHeader    = document.getElementById('siteHeader');
    const mainNav       = document.getElementById('mainNav');
    const sidebar       = document.getElementById('sidebar');
    const sidebarOverlay = document.getElementById('sidebarOverlay');
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebarClose  = document.getElementById('sidebarClose');
    const searchToggle  = document.getElementById('searchToggle');
    const mobileSearch  = document.getElementById('mobileSearch');
    const backToTop     = document.getElementById('backToTop');
    const priceRange    = document.getElementById('priceRange');
    const priceValue    = document.getElementById('priceValue');


    // ═══════════════════════════════════════════════════════
    // 1. Sticky Header Shrink on Scroll
    // ═══════════════════════════════════════════════════════
    let lastScroll = 0;

    function handleScroll() {
        const scrollY = window.scrollY;

        // Header shrink
        if (scrollY > 60) {
            siteHeader.classList.add('scrolled');
        } else {
            siteHeader.classList.remove('scrolled');
        }

        // Back to top button
        if (scrollY > 400) {
            backToTop.classList.add('visible');
        } else {
            backToTop.classList.remove('visible');
        }

        lastScroll = scrollY;
    }

    window.addEventListener('scroll', handleScroll, { passive: true });


    // ═══════════════════════════════════════════════════════
    // 2. Sidebar Toggle (Mobile)
    // ═══════════════════════════════════════════════════════
    function openSidebar() {
        sidebar.classList.add('active');
        sidebarOverlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeSidebar() {
        sidebar.classList.remove('active');
        sidebarOverlay.classList.remove('active');
        document.body.style.overflow = '';
    }

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', openSidebar);
    }
    if (sidebarClose) {
        sidebarClose.addEventListener('click', closeSidebar);
    }
    if (sidebarOverlay) {
        sidebarOverlay.addEventListener('click', closeSidebar);
    }

    // Close sidebar on ESC key
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && sidebar.classList.contains('active')) {
            closeSidebar();
        }
    });


    // ═══════════════════════════════════════════════════════
    // 3. Mobile Search Toggle
    // ═══════════════════════════════════════════════════════
    if (searchToggle && mobileSearch) {
        searchToggle.addEventListener('click', () => {
            mobileSearch.classList.toggle('active');
            if (mobileSearch.classList.contains('active')) {
                const input = mobileSearch.querySelector('.search-input');
                if (input) input.focus();
            }
        });
    }


    // ═══════════════════════════════════════════════════════
    // 4. Back to Top
    // ═══════════════════════════════════════════════════════
    if (backToTop) {
        backToTop.addEventListener('click', () => {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }


    // ═══════════════════════════════════════════════════════
    // 5. Price Range Slider
    // ═══════════════════════════════════════════════════════
    if (priceRange && priceValue) {
        priceRange.addEventListener('input', () => {
            priceValue.textContent = `$${parseInt(priceRange.value).toLocaleString()}`;
        });
    }


    // ═══════════════════════════════════════════════════════
    // 6. Wishlist Button Toggle
    // ═══════════════════════════════════════════════════════
    document.querySelectorAll('.product-card__wishlist').forEach(btn => {
        btn.addEventListener('click', () => {
            const icon = btn.querySelector('i');
            if (icon.classList.contains('bi-heart')) {
                icon.classList.replace('bi-heart', 'bi-heart-fill');
                icon.style.color = '#ef4444';
                btn.style.borderColor = '#ef4444';
            } else {
                icon.classList.replace('bi-heart-fill', 'bi-heart');
                icon.style.color = '';
                btn.style.borderColor = '';
            }
        });
    });


    // ═══════════════════════════════════════════════════════
    // 7. View Toggle (Grid / List)
    // ═══════════════════════════════════════════════════════
    document.querySelectorAll('.view-toggle button').forEach(btn => {
        btn.addEventListener('click', () => {
            document.querySelectorAll('.view-toggle button').forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
        });
    });


    // ═══════════════════════════════════════════════════════
    // 8. Countdown Timer (Deals Banner)
    // ═══════════════════════════════════════════════════════
    const daysEl    = document.getElementById('days');
    const hoursEl   = document.getElementById('hours');
    const minutesEl = document.getElementById('minutes');
    const secondsEl = document.getElementById('seconds');

    if (daysEl && hoursEl && minutesEl && secondsEl) {
        // Set a target date 3 days from now
        const targetDate = new Date();
        targetDate.setDate(targetDate.getDate() + 3);

        function updateTimer() {
            const now  = new Date();
            const diff = targetDate - now;

            if (diff <= 0) {
                daysEl.textContent    = '00';
                hoursEl.textContent   = '00';
                minutesEl.textContent = '00';
                secondsEl.textContent = '00';
                return;
            }

            const d = Math.floor(diff / (1000 * 60 * 60 * 24));
            const h = Math.floor((diff / (1000 * 60 * 60)) % 24);
            const m = Math.floor((diff / (1000 * 60)) % 60);
            const s = Math.floor((diff / 1000) % 60);

            daysEl.textContent    = String(d).padStart(2, '0');
            hoursEl.textContent   = String(h).padStart(2, '0');
            minutesEl.textContent = String(m).padStart(2, '0');
            secondsEl.textContent = String(s).padStart(2, '0');
        }

        updateTimer();
        setInterval(updateTimer, 1000);
    }


    // ═══════════════════════════════════════════════════════
    // 9. Bootstrap Tooltips Init
    // ═══════════════════════════════════════════════════════
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    tooltipTriggerList.forEach(el => {
        new bootstrap.Tooltip(el);
    });


    // ═══════════════════════════════════════════════════════
    // 10. Add to Cart Animation
    // ═══════════════════════════════════════════════════════
    document.querySelectorAll('.btn-add-cart').forEach(btn => {
        btn.addEventListener('click', function () {
            const originalHTML = this.innerHTML;
            this.innerHTML = '<i class="bi bi-check-lg"></i> Added!';
            this.style.background = '#10b981';
            this.disabled = true;

            setTimeout(() => {
                this.innerHTML = originalHTML;
                this.style.background = '';
                this.disabled = false;
            }, 1500);
        });
    });


    // ═══════════════════════════════════════════════════════
    // 11. Active Nav Link Highlight
    // ═══════════════════════════════════════════════════════
    document.querySelectorAll('.sidebar-categories a').forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            document.querySelectorAll('.sidebar-categories a').forEach(l => l.classList.remove('active'));
            this.classList.add('active');
        });
    });

});
