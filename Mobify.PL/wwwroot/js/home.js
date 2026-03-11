/**
 * home.js — Mobify Home Page
 * Only features NOT already covered by site.js:
 *   - Countdown timer  (uses new IDs: timerDays/timerHours/timerMinutes/timerSeconds)
 *   - Newsletter form  feedback
 *   - Cart toast notification
 *   - Sort-select → URL param (stub)
 *   - Apply-filters button feedback
 */

'use strict';

document.addEventListener('DOMContentLoaded', () => {

    // ─── Countdown Timer ─────────────────────────────────────────────────────
    // site.js looks for IDs "days/hours/minutes/seconds".
    // Our banner uses "timerDays/timerHours/timerMinutes/timerSeconds" so no conflict.
    const dealsBanner  = document.getElementById('dealsBanner');
    const timerDays    = document.getElementById('timerDays');
    const timerHours   = document.getElementById('timerHours');
    const timerMins    = document.getElementById('timerMinutes');
    const timerSecs    = document.getElementById('timerSeconds');

    if (timerDays && timerHours && timerMins && timerSecs) {
        const durationHours = parseInt(dealsBanner?.dataset?.endHours || '48', 10);

        // Persist end time across page refreshes in sessionStorage
        const KEY = 'mobify_deal_end';
        let endTime = parseInt(sessionStorage.getItem(KEY) || '0', 10);
        if (!endTime || endTime < Date.now()) {
            endTime = Date.now() + durationHours * 3_600_000;
            sessionStorage.setItem(KEY, endTime);
        }

        const pad = n => String(n).padStart(2, '0');

        function tick() {
            const diff = endTime - Date.now();
            if (diff <= 0) {
                [timerDays, timerHours, timerMins, timerSecs]
                    .forEach(el => el.textContent = '00');
                return;
            }
            timerDays.textContent  = pad(Math.floor(diff / 86_400_000));
            timerHours.textContent = pad(Math.floor((diff % 86_400_000) / 3_600_000));
            timerMins.textContent  = pad(Math.floor((diff % 3_600_000)  / 60_000));
            timerSecs.textContent  = pad(Math.floor((diff % 60_000)     / 1_000));
        }

        tick();
        setInterval(tick, 1_000);
    }


    // ─── Cart Toast ───────────────────────────────────────────────────────────
    const cartToastEl = document.getElementById('cartToast');
    let bsToast = null;
    if (cartToastEl && typeof bootstrap !== 'undefined') {
        bsToast = new bootstrap.Toast(cartToastEl, { delay: 2500 });
    }

    // Extend the base cart-button behaviour (site.js already animates the button;
    // we just add the toast on top).
    document.querySelectorAll('.btn-add-cart').forEach(btn => {
        btn.addEventListener('click', () => {
            bsToast?.show();
            // TODO: replace with real fetch('/Cart/Add', ...) call
        });
    });


    // ─── Newsletter Form ──────────────────────────────────────────────────────
    const form    = document.getElementById('newsletterForm');
    const msgEl   = document.getElementById('newsletterMsg');
    const emailEl = document.getElementById('newsletterEmail');

    if (form && msgEl && emailEl) {
        form.addEventListener('submit', e => {
            e.preventDefault();

            if (!emailEl.value.trim() || !emailEl.checkValidity()) {
                msgEl.textContent = 'Please enter a valid email address.';
                msgEl.className   = 'newsletter-msg error';
                return;
            }

            // TODO: fetch('/Newsletter/Subscribe', { method:'POST', body: ... })
            msgEl.textContent = '🎉 Thanks for subscribing! Check your inbox soon.';
            msgEl.className   = 'newsletter-msg success';
            emailEl.value     = '';

            setTimeout(() => {
                msgEl.textContent = '';
                msgEl.className   = 'newsletter-msg';
            }, 5_000);
        });
    }


    // ─── Sort-Select → URL (stub, uncomment when backend paging is ready) ────
    const sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', () => {
            // const url = new URL(window.location.href);
            // url.searchParams.set('sortBy', sortSelect.value);
            // url.searchParams.set('page', '1');
            // window.location.href = url.toString();
        });
    }


    // ─── Apply Filters Button ─────────────────────────────────────────────────
    const applyBtn = document.getElementById('applyFiltersBtn');
    if (applyBtn) {
        applyBtn.addEventListener('click', () => {
            // TODO: build query params and navigate:
            // const params = new URLSearchParams();
            // params.set('maxPrice', document.getElementById('priceRange')?.value || '');
            // document.querySelectorAll('.brand-filter input:checked')
            //         .forEach(cb => params.append('brands', cb.value));
            // const rating = document.querySelector('.rating-filter input:checked')?.value;
            // if (rating) params.set('rating', rating);
            // window.location.href = '/?' + params.toString();

            // Close the mobile sidebar after applying
            document.getElementById('sidebar')?.classList.remove('active');
            document.getElementById('sidebarOverlay')?.classList.remove('active');
            document.body.style.overflow = '';

            // Brief visual confirmation
            const orig = applyBtn.innerHTML;
            applyBtn.innerHTML = '<i class="bi bi-check-lg me-1"></i> Applied!';
            setTimeout(() => { applyBtn.innerHTML = orig; }, 2_000);
        });
    }

}); // end DOMContentLoaded
