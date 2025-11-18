// js/heart-modal.js

/**
 * Initializes the heart modal functionality.
 * @param {HTMLElement} heartElement - The span element that triggers the modal.
 */
export function initAboutHeartModal(heartElement) {
    if (!heartElement) return;

    // Check if the modal wrapper already exists in the DOM to avoid duplicates
    let modal = document.getElementById("heart-modal");

    if (!modal) {
        // Create modal container
        modal = document.createElement("div");
        modal.id = "heart-modal";
        Object.assign(modal.style, {
            position: "fixed",
            top: "0",
            left: "0",
            width: "100%",
            height: "100%",
            background: "rgba(0,0,0,0.6)",
            display: "none",
            justifyContent: "center",
            alignItems: "center",
            zIndex: "2000",
            backdropFilter: "blur(3px)"
        });

        // Modal content box
        const content = document.createElement("div");
        Object.assign(content.style, {
            background: "#fff",
            padding: "30px",
            borderRadius: "16px",
            textAlign: "center",
            boxShadow: "0 10px 25px rgba(0,0,0,0.2)",
            maxWidth: "300px"
        });

        // Logo image
        const logo = document.createElement("img");
        logo.src = "files/img/logo.png"; 
        logo.alt = "PatrikIT Logo";
        Object.assign(logo.style, {
            width: "100%",
            maxWidth: "180px",
            height: "auto",
            objectFit: "contain",
            cursor: "pointer",
            marginBottom: "15px",
            display: "block",
            marginLeft: "auto",
            marginRight: "auto"
        });

        const hint = document.createElement("p");
        hint.innerHTML = "Thank you for clicking on my heart!<br> It means a lot! <br> 💚<br>Visit my site (patrikit.hu) for more by clicking on my logo.";
        hint.style.color = "#555";
        hint.style.fontSize = "14px";

        content.appendChild(logo);
        content.appendChild(hint);
        modal.appendChild(content);
        document.body.appendChild(modal);

        // Close modal if clicking outside content
        modal.addEventListener("click", (e) => {
            if (e.target === modal) modal.style.display = "none";
        });

        // Click logo to open website
        const openSite = () => window.open("https://patrikit.hu", "_blank");
        logo.addEventListener("click", openSite);
    }

    // Open modal on heart click
    heartElement.addEventListener("click", (e) => {
        e.stopPropagation();
        // Ensure we select the modal again in case variable scope is tricky, though 'modal' is available via closure
        const activeModal = document.getElementById("heart-modal");
        if (activeModal) activeModal.style.display = "flex";
    });
}