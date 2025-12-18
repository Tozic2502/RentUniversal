function Footer() {
    return (
        <footer className="footer">
            {/* Logo section on the left */}
            <div>
                <img
                    src="/Logo.jpg" // Path to the logo image
                    alt="RentAll logo" // Alternative text for the logo image
                    className="footer-logo-img" // CSS class for styling the logo image
                />
            </div>

            {/* Text boxes section on the right */}
            <div className="footer-links">
                {/* About Us section */}
                <div className="footer-box">
                    <h4>Om os</h4> {/* Section heading */}
                    <p>
                        RentAll er en udlejningsplatform, der samler forskelligt udstyr ét sted –
                        fra værktøj og udendørsudstyr til festartikler. Du betaler kun for det,
                        du bruger, når du har brug for det.
                    </p>
                </div>

                {/* Privacy section */}
                <div className="footer-box">
                    <h4>Privatliv</h4> {/* Section heading */}
                    <p>
                        Vi behandler dine personoplysninger fortroligt og bruger dem kun til
                        at håndtere dine bookinger, betalinger og din konto. Alle oplysninger bliver håndteret efter GDPR.
                        Du kan altid kontakte os for indsigt, rettelse eller tilbagekaldelse af Samtykkeerklæring.
                    </p>
                </div>

                {/* Support section */}
                <div className="footer-box">
                    <h4>Support</h4> {/* Section heading */}
                    <p>
                        Har du spørgsmål vedrørende en booking eller udfordringer med din konto, er du velkommen til at kontakte vores support via formularen ovenfor.
                    </p>
                </div>
            </div>
        </footer>
    );
}

export default Footer; // Exporting the Footer component for use in other parts of the application